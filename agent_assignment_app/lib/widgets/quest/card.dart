import 'package:agent_assignment_app/helpers/String.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/widgets/quest/detail/screen.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:just_the_tooltip/just_the_tooltip.dart';

class QuestCard extends StatelessWidget {
  final String id;
  final VoidCallback refresher;
  final String name;
  final String code;
  final String created;
  final int noOfAgent;
  final String expired;
  final QuestStatus status;
  final QuestNecessity necessity;
  const QuestCard(
      {Key? key,
      required this.name,
      required this.expired,
      required this.status,
      required this.necessity,
      required this.code,
      required this.created,
      required this.noOfAgent,
      required this.id,
      required this.refresher})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(10),
          color: const Color.fromRGBO(36, 59, 85, 1),
        ),
        padding: const EdgeInsets.all(10),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Text(
              displayFromSnakeStyle(name).toUpperCase(),
              style: const TextStyle(fontWeight: FontWeight.bold),
            ),
            CardItem(
              tooltip: 'Code',
              icon: Icons.code_rounded,
              child: Container(
                margin: const EdgeInsets.only(bottom: 3),
                child: SelectableText(
                  code,
                  style: const TextStyle(fontWeight: FontWeight.bold),
                ),
              ),
            ),
            CardItem(
              tooltip: 'Necessity',
              icon: Icons.hourglass_empty,
              child: Text(
                necessity.getDisplayStatus(),
                overflow: TextOverflow.ellipsis,
              ),
            ),
            CardItem(
              tooltip: 'Expired date',
              icon: FontAwesomeIcons.triangleExclamation,
              child: Text(
                expired,
                overflow: TextOverflow.ellipsis,
              ),
            ),
            Column(
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Expanded(
                      child: CardItem(
                        tooltip: 'Created',
                        icon: Icons.date_range,
                        child: Text(
                          created,
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ),
                    Expanded(
                      child: CardItem(
                        tooltip: 'Number of agents',
                        icon: Icons.people,
                        child: Text(
                          noOfAgent.toString(),
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ),
                  ],
                ),
              ],
            ),
            const Divider(
              thickness: 2,
            ),
            Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                ElevatedButton(
                    onPressed: () => Navigator.of(context).pushNamed(
                        QuestDetailScreen.route,
                        arguments: {'questId': id, 'refresher': refresher}),
                    child: const Text('Inspect')),
                Row(
                  children: [
                    Container(
                      height: 10.0,
                      width: 10.0,
                      margin: const EdgeInsets.only(right: 5, top: 1),
                      decoration: BoxDecoration(
                        color: status.getColorStatus(),
                        shape: BoxShape.circle,
                      ),
                    ),
                    Text(
                      status.getDisplayStatus(),
                      style: TextStyle(
                          fontSize: 14, color: status.getColorStatus()),
                    )
                  ],
                )
              ],
            )
          ],
        ));
  }
}

class CardItem extends StatelessWidget {
  final Widget child;
  final IconData icon;
  final String tooltip;
  final double minHeight;
  const CardItem(
      {super.key,
      required this.child,
      required this.icon,
      this.tooltip = '',
      this.minHeight = 0});

  @override
  Widget build(BuildContext context) {
    return Container(
      alignment: Alignment.center,
      constraints: BoxConstraints(minHeight: minHeight, maxHeight: 25),
      child: ListTile(
        dense: true,
        minLeadingWidth: 10,
        contentPadding:
            const EdgeInsets.only(left: 0, right: 10, top: 0, bottom: 0),
        leading: JustTheTooltip(
          fadeInDuration: const Duration(milliseconds: 25),
          fadeOutDuration: const Duration(milliseconds: 25),
          offset: 5,
          preferredDirection: AxisDirection.down,
          tailLength: 4,
          tailBaseWidth: 10,
          backgroundColor: const Color.fromRGBO(66, 64, 66, 1),
          content: tooltip.isEmpty
              ? Text(tooltip)
              : Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text(tooltip),
                ),
          child: Icon(
            icon,
            size: 20,
            color: Colors.white,
          ),
        ),
        title: child,
      ),
    );
  }
}
