import 'package:agent_app/helper.dart';
import 'package:agent_app/models/quest.dart';
import 'package:agent_app/widgets/quest_status_dialog.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';

class QuestDetailScreen extends StatelessWidget {
  final Quest quest;
  final Future<void> Function(String) fetchData;
  final void Function(Quest? quest) setSeletedQuest;
  const QuestDetailScreen(this.quest,
      {super.key, required this.fetchData, required this.setSeletedQuest});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(15),
      color: const Color.fromRGBO(58, 66, 86, 1.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Expanded(
                child: _buildFieldItem(
                  icon: FontAwesomeIcons.unity,
                  name: "Code",
                  content: quest.code,
                ),
              ),
              const SizedBox(
                width: 15,
              ),
              Expanded(
                child: _buildFieldItem(
                    icon: Icons.type_specimen,
                    name: "Status",
                    content: QuestStatus.values[quest.questStatus]
                        .getDisplayStatus(),
                    contentBg:
                        QuestStatus.values[quest.questStatus].getColorStatus()),
              ),
            ],
          ),
          _buildFieldItem(
              icon: FontAwesomeIcons.shapes,
              name: "Type",
              content: displayFromSnakeStyle(quest.category.name)),
          Row(
            children: [
              Expanded(
                child: _buildFieldItem(
                  icon: FontAwesomeIcons.calendar,
                  name: "Created",
                  content: DateFormat.yMEd().format(quest.dateCreated),
                ),
              ),
              const SizedBox(
                width: 15,
              ),
              Expanded(
                child: _buildFieldItem(
                  icon: FontAwesomeIcons.calendar,
                  name: "Expired",
                  content: quest.expired == null
                      ? 'None'
                      : DateFormat.yMEd().format(quest.expired!),
                ),
              ),
            ],
          ),
          Row(
            children: [
              Expanded(
                child: _buildFieldItem(
                  icon: FontAwesomeIcons.person,
                  name: "Number of agent",
                  content: '${quest.numberOfAgent.toString()} agent(s)',
                ),
              ),
              const SizedBox(
                width: 15,
              ),
              Expanded(
                child: _buildFieldItem(
                  icon: FontAwesomeIcons.personRunning,
                  name: "Necessity",
                  content:
                      QuestNecessity.values[quest.necessity].getDisplayStatus(),
                ),
              ),
            ],
          ),
          Expanded(
            child: _buildFieldItem(
              icon: FontAwesomeIcons.comment,
              name: "Context",
              contentHeight: 150,
              content: quest.context == null ? 'None' : quest.context!,
            ),
          ),
          Row(
            children: [
              _buildButton(
                  name: 'Back',
                  onPressed: () {
                    setSeletedQuest(null);
                  }),
              const SizedBox(
                width: 15,
              ),
              _buildButton(
                  name: 'Update status',
                  onPressed: QuestStatus.values[quest.questStatus] ==
                              QuestStatus.failed ||
                          QuestStatus.values[quest.questStatus] ==
                              QuestStatus.success
                      ? null
                      : () {
                          showDialog(
                            context: context,
                            builder: (context) {
                              return QuestStatusDialog(
                                questId: quest.id,
                              );
                            },
                          ).then((value) {
                            if (value != null) {
                              fetchData(value);
                            }
                          });
                        }),
            ],
          )
        ],
      ),
    );
  }

  Widget _buildFieldItem({
    required IconData icon,
    required String name,
    required String content,
    double? contentHeight,
    Color contentBg = const Color.fromRGBO(107, 114, 142, 1),
  }) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(children: [
          Icon(
            icon,
            size: 18,
            color: Colors.white,
          ),
          const SizedBox(
            width: 5,
          ),
          Text(
            name,
            style: GoogleFonts.rubik(),
          )
        ]),
        const SizedBox(
          height: 6,
        ),
        Container(
          height: contentHeight,
          width: double.infinity,
          padding: const EdgeInsets.all(8),
          decoration: BoxDecoration(
              color: contentBg,
              borderRadius: const BorderRadius.all(Radius.circular(6))),
          child: SingleChildScrollView(
            child: SizedBox(
              child: Text(
                content.isEmpty ? 'None' : content,
                style: GoogleFonts.robotoSlab(fontSize: 15),
              ),
            ),
          ),
        ),
        const SizedBox(
          height: 12,
        ),
      ],
    );
  }

  Widget _buildButton({required String name, void Function()? onPressed}) {
    return Expanded(
      child: SizedBox(
        height: 50,
        child: ElevatedButton(
            onPressed: onPressed,
            child: Text(
              name,
              textAlign: TextAlign.center,
              style: GoogleFonts.nunito(fontSize: 18),
            )),
      ),
    );
  }
}
