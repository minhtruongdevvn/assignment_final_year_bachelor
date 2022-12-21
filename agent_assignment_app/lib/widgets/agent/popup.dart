import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/predict_result.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:agent_assignment_app/widgets/agent_detail.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class AgentPopup extends StatefulWidget {
  final Agent agent;
  const AgentPopup({super.key, required this.agent});

  @override
  State<AgentPopup> createState() => _AgentPopupState();
}

class _AgentPopupState extends State<AgentPopup> {
  bool assigning = false;
  bool isLoading = false;
  final controller = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      contentPadding: const EdgeInsets.all(0),
      backgroundColor: const Color.fromRGBO(6, 40, 61, 1),
      content: SizedBox(
          height: 400,
          width: 710,
          child: Scaffold(
              floatingActionButtonLocation:
                  FloatingActionButtonLocation.miniEndFloat,
              floatingActionButton: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: isLoading
                    ? []
                    : [
                        if (assigning)
                          SizedBox(
                            height: 40,
                            width: 60,
                            child: ElevatedButton(
                                onPressed: (() {
                                  if (controller.value.text.isEmpty) return;
                                  setState(() {
                                    isLoading = true;
                                  });
                                  QuestAPI.getQuestSuggestedAgentWithCode(
                                          controller.value.text,
                                          widget.agent.id)
                                      .then((value) {
                                    showDialog(
                                      context: context,
                                      builder: (BuildContext context) {
                                        switch (value.metaData['error']) {
                                          case 'isQuestLock':
                                            return _buildQuestLockDialog();
                                          case 'assigned':
                                            return _buildDuplicateAgentDialog();
                                          default:
                                            return _buildConfirmDialog(value);
                                        }
                                      },
                                    ).then((value) => setState(() {
                                          isLoading = false;
                                          assigning = false;
                                        }));
                                  });
                                }),
                                style: ButtonStyle(
                                    shape: MaterialStateProperty.all<
                                            RoundedRectangleBorder>(
                                        RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(5),
                                ))),
                                child: const Text(
                                  'OK',
                                )),
                          ),
                        const SizedBox(width: 10),
                        if (assigning)
                          SizedBox(
                              height: 40,
                              width: 200,
                              child: TextField(
                                controller: controller,
                                decoration: InputDecoration(
                                  floatingLabelBehavior:
                                      FloatingLabelBehavior.never,
                                  labelText: 'Quest code',
                                  labelStyle:
                                      const TextStyle(color: Colors.white),
                                  filled: true,
                                  fillColor:
                                      const Color.fromRGBO(59, 60, 69, 1),
                                  focusedBorder: _buildOutlineInput(),
                                  border: _buildOutlineInput(),
                                  enabledBorder: _buildOutlineInput(),
                                  contentPadding: const EdgeInsets.symmetric(
                                      horizontal: 10),
                                  suffixIcon: const Icon(
                                      FontAwesomeIcons.signature,
                                      size: 15,
                                      color: Colors.white),
                                ),
                              )),
                        const SizedBox(
                          width: 10,
                        ),
                        if (assigning)
                          buildTooltipWrapper(
                            'Close',
                            FloatingActionButton(
                              onPressed: widget.agent.isBusy!
                                  ? null
                                  : () {
                                      setState(() {
                                        assigning = false;
                                      });
                                    },
                              backgroundColor: Colors.red,
                              child: const Icon(FontAwesomeIcons.ban),
                            ),
                            direction: AxisDirection.right,
                          ),
                        if (!assigning)
                          buildTooltipWrapper(
                            widget.agent.isBusy!
                                ? 'Agent is busy'
                                : 'Assign quest',
                            FloatingActionButton(
                              onPressed: widget.agent.isBusy!
                                  ? null
                                  : () {
                                      setState(() {
                                        assigning = true;
                                      });
                                    },
                              backgroundColor: widget.agent.isBusy!
                                  ? Colors.amber
                                  : Colors.green,
                              child: Icon(widget.agent.isBusy!
                                  ? FontAwesomeIcons.exclamation
                                  : FontAwesomeIcons.userPlus),
                            ),
                            direction: AxisDirection.right,
                          )
                      ],
              ),
              backgroundColor: const Color.fromRGBO(6, 40, 61, 1),
              body: Padding(
                padding: const EdgeInsets.all(20),
                child: AgentDetail(widget.agent),
              ))),
    );
  }

  Widget _buildQuestLockDialog() {
    return _buildFailedDialog(
        'This quest is completed', 'You cannot assign any agent on this quest');
  }

  Widget _buildDuplicateAgentDialog() {
    return _buildFailedDialog(
        'Duplicate', 'This agent is already assigned to this quest');
  }

  Widget _buildFailedDialog(String title, String message) {
    return AlertDialog(
      backgroundColor: const Color.fromRGBO(6, 40, 61, 1),
      title: Row(
        children: [
          const Icon(
            FontAwesomeIcons.triangleExclamation,
            size: 20,
            color: Colors.yellow,
          ),
          const SizedBox(
            width: 10,
          ),
          Text(title),
        ],
      ),
      content: SizedBox(
        height: 42,
        child: Container(
          alignment: Alignment.centerLeft,
          child: Text(message),
        ),
      ),
      actions: <Widget>[
        TextButton(
          child: const Text(
            "Close",
            style: TextStyle(color: Colors.white),
          ),
          onPressed: () => Navigator.of(context).pop(),
        ),
      ],
    );
  }

  Widget _buildConfirmDialog(PredictResult value) {
    return AlertDialog(
      backgroundColor: const Color.fromRGBO(6, 40, 61, 1),
      title: const Text('Assign agent'),
      content: SizedBox(
        height: 42,
        child: Column(
          children: [
            Row(
              children: [
                const Text('Success: '),
                Text(value.success ? 'Yes' : 'No')
              ],
            ),
            Row(
              children: [
                const Text('Rate: '),
                Text('${(value.probability * 100).toStringAsFixed(2)}%')
              ],
            )
          ],
        ),
      ),
      actions: <Widget>[
        TextButton(
          child: const Text(
            "Cancel",
            style: TextStyle(color: Colors.white),
          ),
          onPressed: () => Navigator.of(context).pop(),
        ),
        TextButton(
          child: const Text("Yes", style: TextStyle(color: Colors.white)),
          onPressed: () async {
            Navigator.of(context).pop();
            await QuestAPI.addAgentToQuestWithCode(
                controller.value.text, widget.agent.id, value);
          },
        )
      ],
    );
  }

  OutlineInputBorder _buildOutlineInput() {
    return const OutlineInputBorder(
        borderRadius: BorderRadius.all(Radius.circular(3.0)),
        borderSide: BorderSide(color: Color.fromRGBO(59, 60, 69, 1)));
  }
}
