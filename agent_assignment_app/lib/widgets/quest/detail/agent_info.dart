import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:agent_assignment_app/widgets/agent_detail.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

// ignore: must_be_immutable
class AgentInfo extends StatelessWidget {
  final Agent? agent;
  final List<void Function(Agent subject)> refreshers;

  const AgentInfo(this.agent, {super.key, required this.refreshers});

  @override
  Widget build(BuildContext context) {
    var quest = Provider.of<Quest>(context);
    return Container(
      decoration: BoxDecoration(
          color: const Color.fromRGBO(6, 40, 61, 1),
          borderRadius: BorderRadius.circular(10)),
      padding: const EdgeInsets.all(10),
      child: buildDefaultWrapper(
        quest,
        Quest.defaultValue(),
        child: buildNullWrapper(
          agent,
          message: 'No agent is selected',
          builder: (e) {
            return Stack(children: [
              _buildButton(context, e, quest),
              Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Text(
                    'Agent Infomation',
                    style: GoogleFonts.catamaran(
                        fontSize: 25, fontWeight: FontWeight.bold),
                  ),
                  Text(
                    '( ${e.assignedToTheQuest! ? 'Assigned' : 'Unassigned'} )',
                    style: GoogleFonts.catamaran(
                        fontSize: 16, fontWeight: FontWeight.bold),
                  ),
                  Text(
                    'Success Rate: ${(e.predictResult!.probability * 100).toStringAsFixed(1)}%',
                    style: GoogleFonts.catamaran(
                        fontSize: 18, fontWeight: FontWeight.bold),
                  ),
                  Expanded(
                    child: AgentDetail(e),
                  ),
                ],
              ),
            ]);
          },
        ),
      ),
    );
  }

  Widget _buildButton(BuildContext context, Agent agent, Quest quest) {
    final assigned = agent.assignedToTheQuest!;
    Future<void> handlerPress() async {
      if (assigned) {
        quest.currentNumberOfAgent--;
        await QuestAPI.removeAgentFromQuest(quest.id, agent.id);
      } else {
        quest.currentNumberOfAgent++;
        await QuestAPI.addAgentToQuest(
            quest.id, agent.id, agent.predictResult!);
      }
      for (var refresh in refreshers) {
        refresh(agent);
      }
    }

    return SizedBox(
      height: 35,
      child: ElevatedButton.icon(
        style: ButtonStyle(
            backgroundColor: assigned
                ? MaterialStateProperty.all(Colors.red[400])
                : MaterialStateProperty.all(Colors.cyan[900])),
        icon: Icon(
          assigned ? Icons.delete_forever : Icons.arrow_back,
          size: 26.0,
        ),
        label: Text(assigned ? 'Remove' : 'Assign to this quest'),
        onPressed: ((!assigned ||
                        quest.questStatus == QuestStatus.onGoing.index) &&
                    quest.currentNumberOfAgent >= quest.numberOfAgent) ||
                quest.questStatus == QuestStatus.failed.index ||
                quest.questStatus == QuestStatus.success.index
            ? null
            : () {
                Widget cancelButton = TextButton(
                  child: const Text("Cancel"),
                  onPressed: () => Navigator.of(context).pop(),
                );
                Widget continueButton = TextButton(
                  child: const Text("Yes"),
                  onPressed: () {
                    handlerPress().then((value) => Navigator.of(context).pop());
                  },
                );
                AlertDialog alert = AlertDialog(
                  title: Row(
                    children: const [
                      Icon(Icons.warning),
                      Text("Confirmation",
                          style: TextStyle(color: Colors.black87)),
                    ],
                  ),
                  content: Text(
                    assigned
                        ? "Do you want to remove this agent from the quest?"
                        : "Do you want to assign this agent to the current quest?",
                    style: const TextStyle(
                        color: Colors.black87, fontWeight: FontWeight.bold),
                  ),
                  actions: [
                    cancelButton,
                    continueButton,
                  ],
                );
                // show the dialog
                showDialog(
                  context: context,
                  builder: (BuildContext context) {
                    return alert;
                  },
                );
              },
      ),
    );
  }
}
