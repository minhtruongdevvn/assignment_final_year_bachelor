import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/pagination.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:agent_assignment_app/widgets/common_screen.dart';
import 'package:agent_assignment_app/widgets/quest/card.dart';
import 'package:agent_assignment_app/widgets/quest/end_list.dart';
import 'package:agent_assignment_app/widgets/quest/form.dart';
import 'package:agent_assignment_app/widgets/quest/query/drawer.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_animated_dialog/flutter_animated_dialog.dart';
import 'package:flutter_expandable_fab/flutter_expandable_fab.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:intl/intl.dart';
import 'package:loader_overlay/loader_overlay.dart';

class QuestScreen extends StatefulWidget {
  static const String route = 'quests';
  static const pageSize = 12;
  static const page = 1;
  const QuestScreen({super.key});

  @override
  State<QuestScreen> createState() => _QuestScreenState();
}

class _QuestScreenState extends State<QuestScreen> {
  Map<String, dynamic> _currentField = {
    'code': '',
    'numberOfAgent': '',
    'status': null,
    'fromDate': '',
    'toDate': '',
    'categoryId': null,
    'categoryName': null,
    'sort': null,
    'sortMode': QueryDrawer.sortMode[0],
    'dateMode': QueryDrawer.dateMode[0],
    'page': QuestScreen.page,
    'pageSize': QuestScreen.pageSize,
  };

  final ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
  }

  bool isBottom = false;

  void _handleSubmitQuery(Map<String, dynamic> newField) {
    if (mapEquals(_currentField, newField)) return;

    Map<String, dynamic> getFilterMap(Map<String, dynamic> map) {
      return Map<String, dynamic>.from(map)
        ..removeWhere((key, value) => key == 'sort' || key == 'sortMode');
    }

    final currFilterMap = getFilterMap(_currentField);
    final newFilterMap = getFilterMap(newField);

    if (!mapEquals(currFilterMap, newFilterMap)) {
      _scrollController.animateTo(0,
          duration: const Duration(milliseconds: 300),
          curve: Curves.fastOutSlowIn);
      newField.update('page', (value) => QuestScreen.page);
      newField.update('pageSize', (value) => QuestScreen.pageSize);
    }

    setState(() {
      _currentField = newField;
    });
  }

  String _getQuery() {
    final queryBuilder = QueryBuilder();
    queryBuilder
      ..switchPage(_currentField['page'])
      ..switchPageSize(_currentField['pageSize'])
      ..addFilter('Code', _currentField['code'],
          operation: FilterOperation.contain)
      ..addFilter('NumberOfAgent', _currentField['numberOfAgent'])
      ..addFilter('QuestStatus', _currentField['status'])
      ..addFilter('CategoryID', _currentField['categoryId'])
      ..addFilter(_currentField['dateMode']!, _currentField['fromDate'],
          operation: FilterOperation.greaterOrEqual, appendix: 'T00:00:00')
      ..addFilter(_currentField['dateMode']!, _currentField['toDate'],
          operation: FilterOperation.lessOrEqual, appendix: 'T23:59:59')
      ..switchSort(_currentField['sortMode'] == QueryDrawer.sortMode[0]
          ? _currentField['sort'] ?? ''
          : _currentField['sort'] == null
              ? ''
              : '-${_currentField['sort']}');

    return queryBuilder.build();
  }

  @override
  Widget build(BuildContext context) {
    return _buildMainWrapper(context,
        child: FutureBuilder(
            future: QuestAPI.getWithPaging(_getQuery()),
            builder: (ctx, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                context.loaderOverlay.show();
              } else {
                context.loaderOverlay.hide();
              }

              if (snapshot.hasData && snapshot.data != null) {
                final questPaging = snapshot.data!;
                final quests = questPaging.results;

                return Stack(
                  alignment: Alignment.topRight,
                  children: [
                    InkWell(
                      onTap: () {
                        setState(() {});
                      },
                      child: Container(
                        margin: const EdgeInsets.only(right: 25),
                        padding: const EdgeInsets.all(7),
                        decoration: const BoxDecoration(
                            borderRadius: BorderRadius.all(Radius.circular(5)),
                            color: Color.fromRGBO(36, 59, 85, 1)),
                        child: SizedBox(
                          width: 80,
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceAround,
                            children: const [
                              Icon(
                                FontAwesomeIcons.arrowsRotate,
                                color: Colors.white,
                                size: 16,
                              ),
                              Text(
                                "Refresh",
                                style: TextStyle(
                                    color: Colors.white,
                                    fontWeight: FontWeight.bold),
                              ),
                            ],
                          ),
                        ),
                      ),
                    ),
                    Container(
                      margin: const EdgeInsets.only(top: 40),
                      child: Column(children: [
                        Expanded(
                          child: NotificationListener<ScrollNotification>(
                            onNotification: (scrollNotification) {
                              final metrics = scrollNotification.metrics;
                              if (metrics.atEdge &&
                                  scrollNotification is ScrollEndNotification &&
                                  metrics.pixels != 0) {
                                if (questPaging.hasNextPage) {
                                  setState(() {
                                    _currentField.update(
                                        'pageSize',
                                        (value) =>
                                            value + QuestScreen.pageSize);
                                  });
                                }
                              }
                              return true;
                            },
                            child: GridView.builder(
                              controller: _scrollController,
                              itemCount: questPaging.pageRowCount,
                              itemBuilder: (context, index) {
                                final quest = quests[index];
                                final createDay = DateTime.now()
                                    .difference(quest.dateCreated)
                                    .inDays;
                                return QuestCard(
                                  refresher: () => setState(() {}),
                                  id: quest.id,
                                  status: QuestStatus.values[quest.questStatus],
                                  necessity:
                                      QuestNecessity.values[quest.necessity],
                                  name: quest.category.name,
                                  code: quest.code,
                                  created: createDay > 0
                                      ? '$createDay day(s) ago'
                                      : 'Today',
                                  noOfAgent: quest.numberOfAgent,
                                  expired: quest.expired == null
                                      ? 'Not Specify'
                                      : DateFormat('dd/MM/yyyy')
                                          .format(quest.expired!),
                                );
                              },
                              padding: const EdgeInsets.only(
                                  left: 25, right: 25, bottom: 25, top: 0),
                              gridDelegate:
                                  const SliverGridDelegateWithMaxCrossAxisExtent(
                                      maxCrossAxisExtent: 350,
                                      childAspectRatio: 2 /
                                          1.25, // ratio between height and width
                                      crossAxisSpacing:
                                          20, // cross axis is horizontal
                                      mainAxisSpacing: 20),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 20,
                          child: questPaging.hasNextPage
                              ? null
                              : EndOfList(controller: _scrollController),
                        )
                      ]),
                    ),
                  ],
                );
              } else if (snapshot.hasError) {
                return Text('${snapshot.error}');
              }

              // By default, show a loading spinner.
              return Container();
            }));
  }

  Widget _buildMainWrapper(BuildContext context, {required Widget child}) {
    return Scaffold(
      floatingActionButtonLocation: ExpandableFab.location,
      floatingActionButton: buildCommonFAB(
        children: [
          buildTooltipWrapper(
              'Add quest',
              FloatingActionButton.small(
                  heroTag: null,
                  backgroundColor: Colors.green,
                  child: const Icon(Icons.add),
                  onPressed: () => _showQuestForm(
                        context,
                      ))),
          buildTooltipWrapper(
              'Filter',
              FloatingActionButton.small(
                heroTag: null,
                child: const Icon(Icons.search),
                onPressed: () => _showFilter(context),
              )),
        ],
      ),
      body: CommonScreen(
          child: Container(
              margin: const EdgeInsets.all(20),
              width: double.infinity,
              height: double.infinity,
              padding: const EdgeInsets.only(
                  left: 20, right: 20, bottom: 20, top: 7),
              decoration: const BoxDecoration(
                  color: Color.fromRGBO(20, 30, 48, 0.8),
                  borderRadius: BorderRadius.all(Radius.circular(20))),
              child: child)),
    );
  }

  void _showQuestForm(BuildContext context) {
    showModalBottomSheet(
        isDismissible: false,
        context: context,
        isScrollControlled: true,
        builder: (context) {
          return FractionallySizedBox(
            heightFactor: 0.7,
            child: QuestForm(
              refresher: () => setState(() {}),
            ),
          );
        });
  }

  void _showFilter(BuildContext context) {
    showAnimatedDialog(
        duration: const Duration(milliseconds: 300),
        barrierDismissible: true,
        animationType: DialogTransitionType.slideFromRight,
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
              shape: const RoundedRectangleBorder(
                borderRadius: BorderRadius.only(
                    topLeft: Radius.circular(30),
                    bottomLeft: Radius.circular(30)),
              ),
              alignment: Alignment.bottomRight,
              contentPadding: const EdgeInsets.all(0),
              content: Container(
                  alignment: Alignment.bottomRight,
                  width: MediaQuery.of(context).size.width * 0.3,
                  decoration: const BoxDecoration(
                      borderRadius: BorderRadius.only(
                          topLeft: Radius.circular(30),
                          bottomLeft: Radius.circular(30)),
                      gradient: LinearGradient(colors: [
                        Color.fromRGBO(43, 88, 118, 1),
                        Color.fromRGBO(78, 67, 118, 1)
                      ])),
                  child: QueryDrawer(
                    cachedField: _currentField,
                  )));
        }).then((value) {
      if (value != null) {
        _handleSubmitQuery(value as Map<String, dynamic>);
      }
    });
  }
}
