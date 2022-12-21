import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class AssignedTable extends StatefulWidget {
  final String questId;
  final ValueSetter<Agent> updateAgent;
  final List<void Function(Agent subject)> refreshers;
  final Map<String, void Function()> agentReset;
  const AssignedTable(
      {super.key,
      required this.questId,
      required this.updateAgent,
      required this.refreshers,
      required this.agentReset});

  @override
  State<AssignedTable> createState() => _AssignedTableState();
}

class _AssignedTableState extends State<AssignedTable> {
  final ScrollController scrollController = ScrollController();
  String _selectedAgent = '';
  List<Agent> agents = [];
  bool isRefreshed = true;
  @override
  void initState() {
    QuestAPI.getAgentFromQuest(widget.questId).then((value) {
      setState(() {
        agents = value;
      });
    });

    widget.refreshers.add(
        (subject) => QuestAPI.getAgentFromQuest(widget.questId).then((value) {
              setState(() {
                agents = value;
              });
            }));
    widget.agentReset['assigned'] = () => setState(() {
          _selectedAgent = '';
        });

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    final quest = Provider.of<Quest>(context);
    return Container(
        margin: const EdgeInsets.symmetric(vertical: 20),
        padding: const EdgeInsets.all(
          10,
        ),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(10),
          color: const Color.fromRGBO(22, 34, 42, 1),
        ),
        child: buildDefaultWrapper(
          Quest.defaultValue(),
          quest,
          child: buildEmptyWrapper(
            agents,
            message: 'This quest has no agent',
            child: Column(
              children: [
                Text(
                  'Assigned Agent',
                  style: GoogleFonts.catamaran(
                      fontSize: 25, fontWeight: FontWeight.bold),
                ),
                const SizedBox(
                  height: 5,
                ),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Expanded(
                        child: Scrollbar(
                          thumbVisibility: true,
                          controller: scrollController,
                          child: ListView(
                              controller: scrollController,
                              children: agents
                                  .map((agent) => _buildItem(agent.code,
                                      agent.predictResult!.probability, agent))
                                  .toList()),
                        ),
                      ),
                      Container(
                        width: double.infinity,
                        padding: const EdgeInsets.symmetric(
                            vertical: 15, horizontal: 20),
                        decoration: BoxDecoration(
                            color: Colors.transparent.withOpacity(0.3),
                            borderRadius: const BorderRadius.only(
                                bottomLeft: Radius.circular(10),
                                bottomRight: Radius.circular(10))),
                        child: Text(
                          'Amount: ${quest.currentNumberOfAgent}/${quest.numberOfAgent}',
                          style: GoogleFonts.catamaran(fontSize: 20),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ));
  }

  Widget _buildItem(String code, double successRate, Agent agent) {
    return Container(
      margin: const EdgeInsets.symmetric(vertical: 5, horizontal: 20),
      child: Material(
        color: _selectedAgent == agent.code
            ? Colors.transparent.withOpacity(0.3)
            : Colors.transparent,
        borderRadius: BorderRadius.circular(10),
        child: InkWell(
          borderRadius: BorderRadius.circular(10),
          onTap: () {
            if (widget.agentReset['suggested'] != null) {
              widget.agentReset['suggested']!();
            }
            setState(() {
              _selectedAgent = agent.code;
            });
            agent.assignedToTheQuest = true;
            widget.updateAgent(agent);
          },
          child: Container(
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(10),
              color: Colors.transparent.withOpacity(0.1),
            ),
            padding: const EdgeInsets.all(8.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Code',
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    Text(code)
                  ],
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    const Text('Success rate',
                        style: TextStyle(fontWeight: FontWeight.bold)),
                    Text('${(successRate * 100).toStringAsFixed(2)}%')
                  ],
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
