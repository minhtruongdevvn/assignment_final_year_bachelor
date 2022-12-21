import 'package:agent_app/api.dart';
import 'package:agent_app/models/quest.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class QuestStatusDialog extends StatelessWidget {
  final String questId;
  const QuestStatusDialog({super.key, required this.questId});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
        backgroundColor: const Color.fromRGBO(239, 239, 239, 1),
        titlePadding: const EdgeInsets.symmetric(vertical: 10, horizontal: 10),
        contentPadding: const EdgeInsets.symmetric(vertical: 5, horizontal: 10),
        title: Text(
          'Update quest status',
          textAlign: TextAlign.center,
          style: GoogleFonts.montserrat(
              color: Colors.black, fontWeight: FontWeight.bold),
        ),
        content: SingleChildScrollView(
            child: ListBody(
          children: QuestStatus.values
              .where((element) => element != QuestStatus.created)
              .map(
            (e) {
              return _buildItem(e, context);
            },
          ).toList(),
        )));
  }

  Widget _buildItem(QuestStatus status, BuildContext context) {
    return GestureDetector(
      onTap: () {
        API
            .patchUrl('quests/$questId/status/${status.index}')
            .then((_) => Navigator.of(context).pop(status.index.toString()));
      },
      child: Container(
          padding: const EdgeInsets.symmetric(vertical: 12),
          margin: const EdgeInsets.only(bottom: 8),
          decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(10),
              color: const Color.fromRGBO(255, 250, 231, 1)),
          child: Text(
            status.getDisplayStatus(),
            textAlign: TextAlign.center,
            style: GoogleFonts.josefinSans(
                color: status.getColorStatus(), fontWeight: FontWeight.bold),
          )),
    );
  }
}
