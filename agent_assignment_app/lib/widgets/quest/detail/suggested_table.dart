import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:collection/collection.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:loader_overlay/loader_overlay.dart';
import 'package:provider/provider.dart';

class SuggestedTable extends StatefulWidget {
  final ValueSetter<Agent> updateAgent;
  final List<void Function(Agent subject)> refreshers;
  final Map<String, void Function()> agentReset;
  final String questId;
  const SuggestedTable(
      {super.key,
      required this.updateAgent,
      required this.questId,
      required this.refreshers,
      required this.agentReset});

  @override
  State<SuggestedTable> createState() => _SuggestedTableState();
}

class _SuggestedTableState extends State<SuggestedTable> {
  List<Agent> agents = [];
  String _selectedItem = '';
  final ScrollController scrollController = ScrollController();

  @override
  void initState() {
    widget.refreshers.add((subject) => setState(() {
          if (agents.any((element) => subject == element)) {
            agents.remove(subject);
          } else {
            agents.insert(0, subject);
          }
          _selectedItem = '';
        }));
    widget.agentReset['suggested'] = () => setState(() {
          _selectedItem = '';
        });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    final quest = Provider.of<Quest>(context);
    return _buildMainWrapper(
      child: buildQuestLockWrapper(
        Quest.defaultValue(),
        quest,
        child: LayoutBuilder(
          builder: (BuildContext context, BoxConstraints constraints) {
            return Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text(
                  'Possible Agent',
                  style: GoogleFonts.catamaran(
                      fontSize: 25, fontWeight: FontWeight.bold),
                ),
                const SizedBox(
                  height: 8,
                ),
                Expanded(
                  child: agents.isEmpty
                      ? Center(
                          child: SizedBox(
                            height: 50,
                            child: ElevatedButton.icon(
                              onPressed: () {
                                context.loaderOverlay.show();
                                QuestAPI.getQuestSuggestedAgents(widget.questId)
                                    .then((value) => setState(() {
                                          agents = value;
                                          context.loaderOverlay.hide();
                                        }));
                              },
                              icon: const Icon(Icons.download),
                              label: const Text('Get suggested agents'),
                            ),
                          ),
                        )
                      : _buildTable(constraints.maxWidth),
                ),
              ],
            );
          },
        ),
      ),
    );
  }

  Widget _buildTable(double parentWidth) {
    parentWidth = parentWidth - 15; // for padding
    const List<double> allotment = [0.175, 0.3, 0.2, 0.125, 0.17];
    return Scrollbar(
      thumbVisibility: true,
      controller: scrollController,
      child: SingleChildScrollView(
        controller: scrollController,
        child: Column(
          children: [
            _buildRow(
              isHeader: true,
              odd: false,
              children: [
                _buildColumn('Code', allotment[0] * parentWidth,
                    important: true),
                _buildColumn('Full name', allotment[1] * parentWidth,
                    important: true),
                _buildColumn('Birth Date', allotment[2] * parentWidth,
                    important: true),
                _buildColumn('Sex', allotment[3] * parentWidth,
                    important: true),
                _buildColumn('Sucess rate(%)', allotment[4] * parentWidth,
                    important: true),
              ],
            ),
            ...agents.mapIndexed(
              (index, e) {
                return _buildRow(context: e, odd: index.isOdd, children: [
                  _buildColumn(e.code, allotment[0] * parentWidth),
                  _buildColumn('${e.givenName} ${e.familyName}',
                      allotment[1] * parentWidth),
                  _buildColumn(DateFormat('dd/MM/yyyy').format(e.birthDate),
                      allotment[2] * parentWidth),
                  _buildColumn(
                      e.sex ? 'Female' : 'Male', allotment[3] * parentWidth),
                  _buildColumn(
                      (e.predictResult!.probability * 100).toStringAsFixed(3),
                      allotment[4] * parentWidth)
                ]);
              },
            ).toList()
          ],
        ),
      ),
    );
  }

  Widget _buildRow(
      {required List<Widget> children,
      bool odd = true,
      Agent? context,
      bool isHeader = false}) {
    return InkWell(
      onTap: isHeader
          ? null
          : () {
              setState(() {
                widget.agentReset['assigned']!();
                _selectedItem = context?.code ?? '';
                if (context != null) {
                  context.assignedToTheQuest = false;
                  widget.updateAgent(context);
                }
              });
            },
      child: Container(
        margin: EdgeInsets.only(bottom: isHeader ? 5 : 0),
        padding: const EdgeInsets.symmetric(horizontal: 15),
        color: isHeader
            ? Colors.transparent.withOpacity(0.2)
            : context?.code == _selectedItem
                ? Colors.transparent.withOpacity(0.4)
                : odd
                    ? Colors.transparent
                    : Colors.transparent.withOpacity(0.1),
        alignment: Alignment.centerLeft,
        height: 40,
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: children,
        ),
      ),
    );
  }

  Widget _buildColumn(
    String text,
    double width, {
    bool important = false,
  }) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 5),
      alignment: Alignment.centerLeft,
      height: double.infinity,
      width: width,
      child: Text(
        text,
        style: TextStyle(
            fontWeight: important ? FontWeight.bold : FontWeight.normal),
      ),
    );
  }

  Widget _buildMainWrapper({required Widget child}) {
    return Container(
      margin: const EdgeInsets.only(bottom: 20, top: 5),
      decoration: BoxDecoration(
          color: const Color.fromRGBO(0, 43, 91, 1),
          borderRadius: BorderRadius.circular(10)),
      padding: const EdgeInsets.all(10),
      alignment: Alignment.center,
      child: child,
    );
  }
}
