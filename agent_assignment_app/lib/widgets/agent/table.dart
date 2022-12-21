import 'dart:async';

import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/pagination.dart';
import 'package:agent_assignment_app/services/apis/agent.dart';
import 'package:agent_assignment_app/widgets/agent/pagination.dart';
import 'package:agent_assignment_app/widgets/agent/popup.dart';
import 'package:collection/collection.dart';
import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_expandable_fab/flutter_expandable_fab.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:loader_overlay/loader_overlay.dart';

class AgentTable extends StatefulWidget {
  const AgentTable({super.key});

  @override
  State<AgentTable> createState() => _AgentTableState();
}

class _AgentTableState extends State<AgentTable> {
  PageResult<Agent>? agentPaging;
  final SortStatus sortStatus = SortStatus();
  final QueryBuilder queryBuilder = QueryBuilder();
  bool isInit = true;
  Timer? _debounce;
  String? _sex;
  final Map<String, TextEditingController> controllers = {};
  final scrollController = ScrollController();

  List<Agent> get agents {
    if (agentPaging == null) {
      return [];
    }
    return agentPaging!.results;
  }

  void _onSearchChanged(String query, String field, {bool debounce = true}) {
    if (!debounce) {
      queryBuilder.addFilter(field, query, operation: FilterOperation.contain);
      fetchData(context);
      return;
    }
    if (_debounce?.isActive ?? false) _debounce!.cancel();
    _debounce = Timer(const Duration(milliseconds: 1200), () {
      if (query.isEmpty) return;
      queryBuilder.addFilter(field, query, operation: FilterOperation.contain);
      fetchData(context);
    });
  }

  void fetchData(BuildContext context) {
    context.loaderOverlay.show();
    AgentAPI.getWithPaging(queryBuilder.build()).then((value) => setState(
          () {
            agentPaging = value;
            context.loaderOverlay.hide();
          },
        ));
  }

  @override
  void didChangeDependencies() {
    if (isInit) {
      isInit = false;
      fetchData(context);
    }

    super.didChangeDependencies();
  }

  @override
  void dispose() {
    _debounce?.cancel();
    super.dispose();
  }

  void _handleSort() {
    queryBuilder
        .switchSort('${sortStatus.ascending ? '' : '-'}${sortStatus.field}');
    fetchData(context);
  }

  @override
  Widget build(BuildContext context) {
    return _buildMainWrapper(
      builder: (constraints) => [
        Expanded(
          child: Scrollbar(
            controller: scrollController,
            thumbVisibility: true,
            child: SingleChildScrollView(
              controller: scrollController,
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 10),
                child: Column(
                  children: [
                    _buildHeader(
                      constraints.maxWidth,
                      [
                        ['Code', 'Code'],
                        ['Family Name', 'FamilyName'],
                        ['Given Name', 'GivenName'],
                        ['Sex', 'Sex'],
                        ['Height(cm)', 'Height'],
                        ['IQ', 'IQ'],
                        ['EQ', 'EQ'],
                        ['Self Discipline', 'SelfDiscipline'],
                        ['Stamina(%)', 'Stamina'],
                        ['Strength(kg)', 'Strength'],
                        ['Reaction(ms)', 'ReactionTime'],
                      ],
                    ),
                    ...agents
                        .mapIndexed(
                            (i, e) => _buildRow(constraints.maxWidth, i, e.id, [
                                  e.code,
                                  e.familyName,
                                  e.givenName,
                                  e.sex ? 'Female' : 'Male',
                                  e.height.toStringAsFixed(1),
                                  e.iq.toStringAsFixed(1),
                                  e.eq.toStringAsFixed(1),
                                  e.selfDiscipline.toString(),
                                  e.stamina.toStringAsFixed(1),
                                  e.strength.toStringAsFixed(1),
                                  e.reactionTime.toStringAsFixed(1),
                                ]))
                        .toList(),
                  ],
                ),
              ),
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.only(top: 25.0, bottom: 10),
          child: Pagination(
            onPageChange: (page) {
              queryBuilder.switchPage(page);
              queryBuilder.resetFilter();
              queryBuilder.resetSort();
              fetchData(context);
            },
            currentPage: agentPaging?.currentPage ?? 1,
            numberPages: agentPaging?.pageCount ?? 1,
          ),
        )
      ],
    );
  }

  Widget _buildMainWrapper(
      {required List<Widget> Function(BoxConstraints) builder}) {
    return Scaffold(
      backgroundColor: Colors.transparent,
      floatingActionButtonLocation: ExpandableFab.location,
      floatingActionButton: buildCommonFAB(
        margin: const EdgeInsets.all(0),
        children: [
          buildTooltipWrapper(
              'Clear filter',
              FloatingActionButton.small(
                heroTag: null,
                backgroundColor: const Color.fromRGBO(185, 46, 52, 1),
                child: const Icon(
                  FontAwesomeIcons.filterCircleXmark,
                  size: 20,
                ),
                onPressed: () {
                  queryBuilder.resetFilter();
                  setState(() {
                    _sex = null;
                    controllers.forEach((key, value) {
                      value.clear();
                    });
                  });
                  fetchData(context);
                },
              )),
          buildTooltipWrapper(
              'Clear sort',
              FloatingActionButton.small(
                heroTag: null,
                backgroundColor: const Color.fromRGBO(185, 46, 52, 1),
                child: const Icon(FontAwesomeIcons.sort),
                onPressed: () {
                  queryBuilder.resetSort();
                  setState(() {
                    sortStatus.field = null;
                    sortStatus.ascending = true;
                  });
                  fetchData(context);
                },
              )),
        ],
      ),
      body: Container(
        margin: const EdgeInsets.symmetric(vertical: 20, horizontal: 10),
        child: LayoutBuilder(
          builder: (BuildContext context, BoxConstraints constraints) => Column(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: builder(constraints),
          ),
        ),
      ),
    );
  }

  Widget _buildHeader(double width, List<List<String>> headers) {
    return SizedBox(
        height: 80,
        child: GridView(
          physics: const NeverScrollableScrollPhysics(),
          gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
              childAspectRatio: width / headers.length / 80,
              maxCrossAxisExtent: width / headers.length),
          children: headers.map((e) {
            controllers.putIfAbsent(e[1], () => TextEditingController());
            var controller = controllers[e[1]];
            return InkWell(
              child: Container(
                padding: const EdgeInsets.all(8),
                decoration: const BoxDecoration(
                    color: Color.fromRGBO(43, 44, 52, 1),
                    border: Border.symmetric(
                        vertical: BorderSide(
                            width: 1, color: Color.fromRGBO(39, 40, 49, 1)))),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Row(
                      mainAxisSize: MainAxisSize.min,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Padding(
                          padding: const EdgeInsets.symmetric(vertical: 3.0),
                          child: Text(
                            e[0],
                            style: const TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ),
                        const SizedBox(
                          width: 5,
                        ),
                        if (sortStatus.field != null &&
                            sortStatus.field == e[1])
                          Icon(
                            sortStatus.ascending
                                ? FontAwesomeIcons.arrowUp
                                : FontAwesomeIcons.arrowDown,
                            size: 15,
                            color: Colors.white,
                          )
                      ],
                    ),
                    const SizedBox(
                      height: 8,
                    ),
                    Expanded(
                      child: Container(
                        alignment: Alignment.centerLeft,
                        child: e[1] == 'Sex'
                            ? DropdownButtonHideUnderline(
                                child: DropdownButton2(
                                  items: ['Female', 'Male']
                                      .map((item) => DropdownMenuItem<String>(
                                            value: item,
                                            child: Text(
                                              item,
                                            ),
                                          ))
                                      .toList(),
                                  value: _sex,
                                  onChanged: (value) {
                                    setState(() {
                                      _sex = value as String;
                                    });
                                    _onSearchChanged(
                                        _sex == 'Female' ? 'True' : 'False',
                                        'Sex',
                                        debounce: false);
                                  },
                                  dropdownOverButton: true,
                                  dropdownDecoration: BoxDecoration(
                                      color:
                                          const Color.fromRGBO(59, 60, 69, 1),
                                      borderRadius: const BorderRadius.all(
                                          Radius.circular(3.0)),
                                      border: Border.all(
                                          color: const Color.fromRGBO(
                                              59, 60, 69, 1))),
                                  iconEnabledColor: Colors.white,
                                  buttonPadding:
                                      const EdgeInsets.only(left: 10),
                                  buttonDecoration: BoxDecoration(
                                      color:
                                          const Color.fromRGBO(59, 60, 69, 1),
                                      borderRadius: const BorderRadius.all(
                                          Radius.circular(3.0)),
                                      border: Border.all(
                                          color: const Color.fromRGBO(
                                              59, 60, 69, 1))),
                                  buttonHeight: double.infinity,
                                  buttonWidth: double.infinity,
                                  itemHeight: 30,
                                ),
                              )
                            : TextField(
                                controller: controller,
                                onChanged: (value) =>
                                    _onSearchChanged(value, e[1]),
                                textAlignVertical: TextAlignVertical.center,
                                decoration: InputDecoration(
                                  filled: true,
                                  fillColor:
                                      const Color.fromRGBO(59, 60, 69, 1),
                                  focusedBorder: _buildOutlineInput(),
                                  border: _buildOutlineInput(),
                                  enabledBorder: _buildOutlineInput(),
                                  contentPadding: const EdgeInsets.symmetric(
                                      horizontal: 10),
                                  suffixIcon: const Icon(
                                      FontAwesomeIcons.magnifyingGlass,
                                      size: 15,
                                      color: Colors.white),
                                ),
                              ),
                      ),
                    ),
                  ],
                ),
              ),
              onTap: () {
                setState(() {
                  if (sortStatus.field == e[1]) {
                    sortStatus.ascending = !sortStatus.ascending;
                  }
                  sortStatus.field = e[1];
                });
                _handleSort();
              },
            );
          }).toList(),
        ));
  }

  Widget _buildRow(
    double width,
    int index,
    String id,
    List<String> values,
  ) {
    return InkWell(
      onTap: () => _showAgentInfo(id),
      child: Container(
        decoration: BoxDecoration(
          color: index.isOdd
              ? const Color.fromRGBO(43, 44, 52, 1)
              : const Color.fromRGBO(46, 47, 58, 1),
        ),
        height: 50,
        child: GridView(
          physics: const NeverScrollableScrollPhysics(),
          gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
              childAspectRatio: width / values.length / 50,
              maxCrossAxisExtent: width / values.length),
          children: values.map((e) {
            return Container(
                decoration: const BoxDecoration(
                    border: Border.symmetric(
                        vertical: BorderSide(
                            width: 1, color: Color.fromRGBO(39, 40, 49, 1)))),
                alignment: Alignment.centerLeft,
                padding: const EdgeInsets.all(8),
                child: Text(e));
          }).toList(),
        ),
      ),
    );
  }

  void _showAgentInfo(String id) {
    context.loaderOverlay.show();
    AgentAPI.getById(id).then((value) {
      context.loaderOverlay.hide();
      showDialog(
          context: context,
          builder: (BuildContext context) {
            return AgentPopup(agent: value);
          });
    });
  }

  OutlineInputBorder _buildOutlineInput() {
    return const OutlineInputBorder(
        borderRadius: BorderRadius.all(Radius.circular(3.0)),
        borderSide: BorderSide(color: Color.fromRGBO(59, 60, 69, 1)));
  }
}

class SortStatus {
  String? field;
  bool ascending = true;
}
