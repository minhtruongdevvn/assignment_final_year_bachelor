import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:collection/collection.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:timeline_tile/timeline_tile.dart';

class QuestTransaction extends StatelessWidget {
  QuestTransaction({super.key});

  final ScrollController scrollController = ScrollController();

  @override
  Widget build(BuildContext context) {
    final quest = Provider.of<Quest>(context);
    final transactions = quest.transactions ?? [];
    return Container(
      margin: const EdgeInsets.only(top: 20, bottom: 20, right: 10),
      padding: const EdgeInsets.all(10),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(10),
        gradient: const LinearGradient(
          begin: Alignment.topCenter,
          end: Alignment.bottomLeft,
          colors: [
            Color.fromRGBO(58, 96, 115, 1),
            Color.fromRGBO(22, 34, 42, 1),
          ],
        ),
      ),
      child: buildDefaultWrapper(
        Quest.defaultValue(),
        quest,
        child: buildEmptyWrapper(
          transactions,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text(
                'Transaction',
                style: GoogleFonts.catamaran(
                    fontSize: 25, fontWeight: FontWeight.bold),
              ),
              const SizedBox(
                height: 10,
              ),
              Expanded(
                child: Scrollbar(
                  thumbVisibility: true,
                  controller: scrollController,
                  child: ListView(
                    controller: scrollController,
                    children: transactions.mapIndexed((index, transaction) {
                      if (index == 0) {
                        _buildTransactionTile(
                          isFirst: true,
                          date: transaction.dateCreated,
                          questStatus: transaction.questStatus,
                        );
                      }
                      if (index == quest.transactions!.length - 1) {
                        return _buildTransactionTile(
                          isLast: true,
                          date: transaction.dateCreated,
                          questStatus: transaction.questStatus,
                        );
                      }
                      return _buildTransactionTile(
                        date: transaction.dateCreated,
                        questStatus: transaction.questStatus,
                      );
                    }).toList(),
                  ),
                ),
              )
            ],
          ),
        ),
      ),
    );
  }

  TimelineTile _buildTransactionTile({
    required DateTime date,
    required int questStatus,
    bool isFirst = true,
    bool isLast = false,
  }) {
    final status = QuestStatus.values[questStatus];
    return TimelineTile(
      alignment: TimelineAlign.manual,
      lineXY: 0.4,
      beforeLineStyle: LineStyle(color: Colors.white.withOpacity(0.7)),
      indicatorStyle: IndicatorStyle(
        indicatorXY: 0.3,
        drawGap: true,
        width: 30,
        height: 30,
        indicator: _IconIndicator(
          iconData: status.getIconStatus(),
          size: 20,
        ),
      ),
      isLast: isLast,
      isFirst: isFirst,
      startChild: Center(
        child: Container(
          alignment: const Alignment(0.0, -0.60),
          child: Column(
            children: [
              Text(
                DateFormat.yMd().format(date),
                style: GoogleFonts.lato(
                  fontSize: 15,
                  color: Colors.white.withOpacity(0.6),
                  fontWeight: FontWeight.w500,
                ),
              ),
              Text(
                DateFormat.Hm().format(date),
                style: GoogleFonts.adamina(
                  fontSize: 12,
                  color: Colors.white.withOpacity(0.6),
                  fontWeight: FontWeight.w500,
                ),
              ),
            ],
          ),
        ),
      ),
      endChild: Padding(
        padding: const EdgeInsets.only(left: 16, right: 10, top: 7, bottom: 10),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Text(
              status.getDisplayStatus(),
              style: GoogleFonts.lato(
                fontSize: 18,
                color: status.getColorStatus(),
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 20),
          ],
        ),
      ),
    );
  }
}

class _IconIndicator extends StatelessWidget {
  const _IconIndicator({
    Key? key,
    required this.iconData,
    required this.size,
  }) : super(key: key);

  final IconData iconData;
  final double size;

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        Container(
          decoration: BoxDecoration(
            shape: BoxShape.circle,
            color: Colors.white.withOpacity(0.7),
          ),
        ),
        Positioned.fill(
          child: Align(
            alignment: Alignment.center,
            child: SizedBox(
              height: 30,
              width: 30,
              child: Icon(
                iconData,
                size: size,
                color: const Color(0xFF9E3773).withOpacity(0.7),
              ),
            ),
          ),
        ),
      ],
    );
  }
}
