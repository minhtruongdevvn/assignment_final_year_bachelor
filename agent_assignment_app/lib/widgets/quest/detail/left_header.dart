import 'package:agent_assignment_app/helpers/string.dart';
import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

class LeftDetailHeader extends StatelessWidget {
  const LeftDetailHeader({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    final quest = Provider.of<Quest>(context);
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 10, horizontal: 5),
      child: buildDefaultWrapper(
        Quest.defaultValue(),
        quest,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            PropertyCard(
              maxWidth: 85,
              title: 'Status',
              value: QuestStatus.values[quest.questStatus].getDisplayStatus(),
              valueTextStyle: TextStyle(
                  color:
                      QuestStatus.values[quest.questStatus].getColorStatus()),
            ),
            const SizedBox(
              width: 30,
            ),
            PropertyCard(
                title: 'Expired',
                value: quest.expired == null
                    ? 'Not specify'
                    : DateFormat.yMd().format(quest.expired!)),
            const SizedBox(
              width: 30,
            ),
            PropertyCard(
              title: 'Code',
              value: quest.code,
              selectable: true,
            ),
            const SizedBox(
              width: 20,
            ),
            PropertyCard(
              title: 'Category',
              value: displayFromSnakeStyle(quest.category.name),
              description: quest.category.description,
            ),
          ],
        ),
      ),
    );
  }
}

class PropertyCard extends StatelessWidget {
  final String title;
  final String value;
  final TextStyle? valueTextStyle;
  final String description;
  final bool selectable;
  final double? maxWidth;
  const PropertyCard(
      {Key? key,
      required this.title,
      required this.value,
      this.selectable = false,
      this.description = '',
      this.valueTextStyle,
      this.maxWidth})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 10),
      height: 150,
      constraints: const BoxConstraints(maxWidth: 320),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Text(title,
              maxLines: 1,
              style: GoogleFonts.oswald(
                  fontWeight: FontWeight.bold,
                  color: Colors.white,
                  fontSize: 18)),
          const SizedBox(
            height: 5,
          ),
          Container(
            constraints: BoxConstraints(maxWidth: maxWidth ?? double.infinity),
            child: selectable
                ? SelectableText(
                    value,
                    style: valueTextStyle == null
                        ? const TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 17)
                        : valueTextStyle!.copyWith(
                            fontWeight: FontWeight.bold, fontSize: 17),
                  )
                : Text(
                    value,
                    style: valueTextStyle == null
                        ? const TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 17)
                        : valueTextStyle!.copyWith(
                            fontWeight: FontWeight.bold, fontSize: 17),
                  ),
          ),
          const SizedBox(
            height: 5,
          ),
          if (description.isNotEmpty)
            Expanded(
              child: Container(
                constraints: const BoxConstraints(maxWidth: 295),
                child: SingleChildScrollView(
                  child: Text(
                    description,
                    style: const TextStyle(
                        fontWeight: FontWeight.bold, fontSize: 13),
                  ),
                ),
              ),
            ),
        ],
      ),
    );
  }
}
