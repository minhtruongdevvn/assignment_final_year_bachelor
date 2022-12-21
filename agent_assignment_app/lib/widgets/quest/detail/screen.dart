import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:agent_assignment_app/widgets/common_screen.dart';
import 'package:agent_assignment_app/widgets/quest/detail/agent_info.dart';
import 'package:agent_assignment_app/widgets/quest/detail/assigned_table.dart';
import 'package:agent_assignment_app/widgets/quest/detail/context.dart';
import 'package:agent_assignment_app/widgets/quest/detail/left_header.dart';
import 'package:agent_assignment_app/widgets/quest/detail/suggested_table.dart';
import 'package:agent_assignment_app/widgets/quest/detail/transaction.dart';
import 'package:agent_assignment_app/widgets/quest/form.dart';
import 'package:flutter/material.dart';
import 'package:flutter_expandable_fab/flutter_expandable_fab.dart';
import 'package:loader_overlay/loader_overlay.dart';
import 'package:provider/provider.dart';

// ignore: must_be_immutable
class QuestDetailScreen extends StatelessWidget {
  static const String route = 'quests/detail';
  final _selectedAgent = ValueNotifier<Agent?>(null);

  late List<void Function(Agent subject)> refreshers = [
    (subject) => updateAgent(null)
  ];

  Map<String, void Function()> resetSelectedAgent = {};

  QuestDetailScreen({
    super.key,
  });

  void updateAgent(Agent? agent) => _selectedAgent.value = agent;

  @override
  Widget build(BuildContext context) {
    final routeData =
        ModalRoute.of(context)!.settings.arguments as Map<String, dynamic>;
    final questId = routeData['questId'] as String;
    final refresher = routeData['refresher'] as void Function();
    return FutureProvider<Quest>.value(
      initialData: Quest.defaultValue(),
      value: QuestAPI.getQuestById(questId),
      builder: (context, child) {
        return Consumer<Quest>(builder: (context, value, __) {
          return _buildWigetWrapper(context, questId, value, refresher,
              child: child!);
        });
      },
      child: _buildMainWrapper(context, childrens: [
        _buildLeftBlock(context, questId),
        _buildRightBlock(questId)
      ]),
    );
  }

  Widget _buildWigetWrapper(
      BuildContext context, String questId, Quest quest, VoidCallback refresher,
      {required Widget child}) {
    return Scaffold(
        floatingActionButtonLocation: ExpandableFab.location,
        floatingActionButton: buildCommonFAB(
          type: ExpandableFabType.fan,
          children: [
            if (quest.questStatus != QuestStatus.failed.index &&
                quest.questStatus != QuestStatus.success.index)
              buildTooltipWrapper(
                  direction: AxisDirection.left,
                  'Edit this quest',
                  FloatingActionButton.small(
                      heroTag: null,
                      backgroundColor: Colors.yellow.shade800,
                      child: const Icon(Icons.edit),
                      onPressed: () => _showQuestForm(context, refresher))),
            buildTooltipWrapper(
                direction: AxisDirection.left,
                'Delete this quest',
                FloatingActionButton.small(
                    heroTag: null,
                    backgroundColor: Colors.redAccent.shade700,
                    child: const Icon(Icons.delete_forever),
                    onPressed: () {
                      context.loaderOverlay.show();
                      QuestAPI.deleteQuest(questId).then((_) {
                        Navigator.pop(context);
                        context.loaderOverlay.hide();
                        refresher();
                      });
                    })),
            buildTooltipWrapper(
                direction: AxisDirection.left,
                'Back to quest list',
                FloatingActionButton.small(
                    heroTag: null,
                    child: const Icon(Icons.arrow_back),
                    onPressed: () {
                      Navigator.of(context).pop();
                    })),
          ],
        ),
        body: child);
  }

  void _showQuestForm(BuildContext context, VoidCallback refresher) {
    final quest = Provider.of<Quest>(context, listen: false);
    showModalBottomSheet(
        isDismissible: false,
        context: context,
        isScrollControlled: true,
        builder: (context) {
          return FractionallySizedBox(
            heightFactor: 0.7,
            child: QuestForm(
              questEdit: quest,
              refresher: refresher,
            ),
          );
        });
  }

  Widget _buildMainWrapper(BuildContext context,
      {required List<Widget> childrens}) {
    return CommonScreen(
        child: Container(
      height: double.infinity,
      width: double.infinity,
      margin: const EdgeInsets.all(20),
      padding: const EdgeInsets.only(left: 25, right: 25, top: 20),
      decoration: BoxDecoration(
          color: const Color.fromRGBO(39, 50, 58, 1),
          borderRadius: BorderRadius.circular(10)),
      child: Row(
        children: childrens.map((e) => Expanded(child: e)).toList(),
      ),
    ));
  }

  Widget _buildLeftBlock(BuildContext context, String questId) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Expanded(flex: 3, child: LeftDetailHeader()),
        Expanded(flex: 3, child: QuestContext()),
        Expanded(
          flex: 6,
          child: Row(
            children: [
              Expanded(
                flex: 6,
                child: QuestTransaction(),
              ),
              Expanded(
                flex: 5,
                child: AssignedTable(
                  agentReset: resetSelectedAgent,
                  refreshers: refreshers,
                  updateAgent: updateAgent,
                  questId: questId,
                ),
              )
            ],
          ),
        ),
      ],
    );
  }

  Widget _buildRightBlock(String questId) {
    return Container(
      padding: const EdgeInsets.only(left: 30),
      margin: const EdgeInsets.only(
        bottom: 20,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Expanded(
              child: SuggestedTable(
            agentReset: resetSelectedAgent,
            refreshers: refreshers,
            questId: questId,
            updateAgent: updateAgent,
          )), // todo
          Expanded(
              child: ValueListenableBuilder(
                  valueListenable: _selectedAgent,
                  builder: (context, value, child) {
                    return AgentInfo(refreshers: refreshers, value);
                  })) // todo
        ],
      ),
    );
  }
}
