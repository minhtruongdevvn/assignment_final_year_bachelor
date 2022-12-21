import 'dart:async';

import 'package:agent_app/api.dart';
import 'package:agent_app/constant.dart';
import 'package:agent_app/env.dart';
import 'package:agent_app/helper.dart';
import 'package:agent_app/models/quest.dart';
import 'package:agent_app/notification_service.dart';
import 'package:agent_app/providers/auth.dart';
import 'package:agent_app/widgets/quest_detail_screen.dart';
import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:signalr_netcore/signalr_client.dart';

class HomeScreen extends StatefulWidget {
  final String? questCode;
  const HomeScreen({super.key, this.questCode});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  final hubConnection = HubConnectionBuilder()
      .withUrl('${EnvironmentConfig.envVars.aamUrl}/quest-notify',
          options: HttpConnectionOptions(
            accessTokenFactory: () async => Future.value(Auth.token),
            transport: HttpTransportType.WebSockets,
            logMessageContent: true,
          ))
      .build();
  final notificationService = NotificationService();
  final codeController = TextEditingController();
  Timer? _debounce;
  bool isInit = true;
  String status = QuestStatus.waiting.index.toString();
  List<Quest> quests = [];
  Quest? selectedQuest;
  String? updatedQuestCode;

  _onSearchChanged(String query) {
    if (_debounce?.isActive ?? false) _debounce!.cancel();
    _debounce = Timer(const Duration(milliseconds: 500), () {
      _fetchData();
    });
  }

  void _setSeletedQuest(Quest? quest) {
    setState(() {
      selectedQuest = quest;
    });
  }

  Future<void> _fetchData([String? withStatus]) async {
    final List<String> filterStrings = [];
    if (codeController.text.isNotEmpty) {
      filterStrings.add('code=${codeController.text}');
    }
    if (status.isNotEmpty && withStatus == null) {
      filterStrings.add('status=$status');
    } else if (withStatus != null) {
      filterStrings.add('status=$withStatus');
    }

    final filter = filterStrings.join(',');
    final responseWrapper = await API.getUrl(
        'agents/${Auth.userId}/quests${filter.isEmpty ? '' : '/$filter'}');
    final responseData = responseWrapper.getBody();
    setState(() {
      quests = List<Map<String, dynamic>>.from(responseData)
          .map((e) => Quest.fromJson(e))
          .toList();

      if (withStatus != null) {
        status = withStatus;
        updatedQuestCode = selectedQuest!.code;
        selectedQuest!.questStatus = int.parse(status);
        return;
      }

      if (selectedQuest != null) {
        selectedQuest =
            quests.firstWhere((element) => element.id == selectedQuest!.id);
      }
    });
  }

  @override
  void didChangeDependencies() {
    if (isInit) {
      if (widget.questCode != null) {
        codeController.text = widget.questCode!;
      }
      _fetchData();
      notificationService.init(context);
      hubConnection.on(HubAction.update, (arguments) {
        final questCode = arguments!.first as String;
        notificationService.showMessage(
            'Some quest that you are in is updated', 1, questCode);

        _fetchData();
      });
      hubConnection.on(HubAction.add, (arguments) {
        final questCode = arguments!.first as String;
        notificationService.showMessage(
            'You has been assigned a new quest', 2, questCode);
        _fetchData();
      });
      hubConnection.on(HubAction.delete, (arguments) {
        notificationService.showMessage('One of your quest is delete', 3);
        final questId = arguments?.first as String?;
        _fetchData();
        if (questId != null &&
            selectedQuest != null &&
            questId == selectedQuest!.id) {
          _setSeletedQuest(null);
        }
      });
      hubConnection.start();

      isInit = false;
    }

    super.didChangeDependencies();
  }

  @override
  void dispose() {
    hubConnection.stop();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    ListTile makeListTile(Quest quest) => ListTile(
        onTap: () async {
          setState(() {
            selectedQuest = quest;
          });
        },
        isThreeLine: true,
        contentPadding: const EdgeInsets.symmetric(
          horizontal: 20.0,
        ),
        leading: Container(
          padding: const EdgeInsets.only(right: 12.0),
          decoration: const BoxDecoration(
              border:
                  Border(right: BorderSide(width: 1.0, color: Colors.white24))),
          child: Icon(QuestStatus.values[quest.questStatus].getIconStatus(),
              color: QuestStatus.values[quest.questStatus].getColorStatus()),
        ),
        title: Text(
          displayFromSnakeStyle(quest.category.name),
          style:
              const TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        // subtitle: Text("Intermediate", style: TextStyle(color: Colors.white)),
        subtitle: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Row(
                  children: [
                    const Icon(
                      Icons.people,
                      color: Colors.white,
                    ),
                    Padding(
                        padding: const EdgeInsets.only(left: 10.0),
                        child: Text(quest.numberOfAgent.toString(),
                            style: const TextStyle(color: Colors.white))),
                  ],
                ),
                Text(
                  "Expired: ${quest.expired == null ? 'None' : DateFormat.yMd().format(quest.expired!)}",
                  style: GoogleFonts.sourceSansPro(),
                )
              ],
            ),
            Text(
              "CODE:  ${quest.code}",
              style: GoogleFonts.sourceSansPro(),
            ),
          ],
        ),
        trailing: Text(
          QuestNecessity.values[quest.necessity].getDisplayStatus(),
          style: GoogleFonts.rubikMicrobe(),
        ));

    Card makeCard(Quest quest) => Card(
        elevation: 8.0,
        margin: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 6.0),
        child: Container(
          decoration: BoxDecoration(
              color: updatedQuestCode != null && updatedQuestCode == quest.code
                  ? const Color.fromRGBO(64, 75, 96, 0.65)
                  : const Color.fromRGBO(64, 75, 96, 0.9)),
          child: makeListTile(quest),
        ));

    final makeBody = Stack(
      children: [
        Column(
          children: [
            Container(
              margin: const EdgeInsets.symmetric(horizontal: 12),
              height: 52,
              child: Row(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Expanded(
                    child: Container(
                      margin: const EdgeInsets.only(bottom: 10.5),
                      child: TextField(
                        controller: codeController,
                        onChanged: _onSearchChanged,
                        style: GoogleFonts.robotoSlab(
                            color: Colors.white, fontSize: 13),
                        decoration: InputDecoration(
                            focusedBorder: const UnderlineInputBorder(
                              borderSide: BorderSide(color: Colors.white),
                            ),
                            enabledBorder: const UnderlineInputBorder(
                              borderSide: BorderSide(color: Colors.white),
                            ),
                            hintText: 'Filter by code',
                            hintStyle: GoogleFonts.robotoSlab(
                                color: Colors.white, fontSize: 13)),
                      ),
                    ),
                  ),
                  const SizedBox(
                    width: 15,
                  ),
                  Expanded(
                    child: DropdownButton2(
                      buttonWidth: double.infinity,
                      iconEnabledColor: Colors.white,
                      underline: Container(
                        height: 1.5,
                        color: Colors.white70,
                      ),
                      buttonPadding: const EdgeInsets.all(0),
                      style: GoogleFonts.robotoSlab(
                          color: Colors.white, fontSize: 13),
                      buttonDecoration: const BoxDecoration(
                        color: Colors.transparent,
                      ),
                      dropdownDecoration:
                          const BoxDecoration(color: Colors.grey),
                      hint: const Text(
                        'Filter by status',
                        style: TextStyle(
                          fontSize: 13,
                          color: Colors.white,
                        ),
                      ),
                      items: QuestStatus.values
                          .where((e) => e != QuestStatus.created)
                          .map((item) {
                        return DropdownMenuItem<String>(
                          value: item.index.toString(),
                          child: Text(
                            item.getDisplayStatus(),
                            style: const TextStyle(
                                fontSize: 14, color: Colors.white),
                          ),
                        );
                      }).toList(),
                      value: status.isEmpty ? null : status,
                      onChanged: (value) {
                        status = value as String;
                        _fetchData();
                      },
                    ),
                  )
                ],
              ),
            ),
            Expanded(
              child: ListView.builder(
                scrollDirection: Axis.vertical,
                shrinkWrap: true,
                itemCount: quests.length,
                itemBuilder: (BuildContext context, int index) {
                  return makeCard(quests[index]);
                },
              ),
            )
          ],
        ),
        if (selectedQuest != null)
          QuestDetailScreen(
            selectedQuest!,
            setSeletedQuest: _setSeletedQuest,
            fetchData: _fetchData,
          )
      ],
    );

    final topAppBar = AppBar(
      actions: [
        IconButton(
            onPressed: () {
              Provider.of<Auth>(context, listen: false).logout();
            },
            icon: const Icon(FontAwesomeIcons.arrowRightFromBracket))
      ],
      elevation: 0.1,
      backgroundColor: const Color.fromRGBO(58, 66, 86, 1.0),
      title: const Text("Quests"),
    );

    return Scaffold(
      backgroundColor: const Color.fromRGBO(58, 66, 86, 1.0),
      appBar: topAppBar,
      body: makeBody,
    );
  }
}
