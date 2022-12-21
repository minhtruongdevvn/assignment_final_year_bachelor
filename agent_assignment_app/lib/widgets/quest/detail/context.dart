import 'package:agent_assignment_app/helpers/widget.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';

class QuestContext extends StatelessWidget {
  QuestContext({
    super.key,
  });
  final ScrollController contextScroll = ScrollController();

  @override
  Widget build(BuildContext context) {
    final quest = Provider.of<Quest>(context);
    return Container(
      padding: const EdgeInsets.all(10),
      decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(10),
          color: Colors.blueAccent.withOpacity(0.1)),
      child: buildDefaultWrapper(
        Quest.defaultValue(),
        quest,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text(
              'Context',
              style: GoogleFonts.catamaran(
                  fontSize: 25, fontWeight: FontWeight.bold),
            ),
            const SizedBox(
              height: 10,
            ),
            SizedBox(
              height: 100,
              width: double.infinity,
              child: (quest.context == null || quest.context!.isEmpty)
                  ? Center(
                      child: Text('Not specify',
                          style: GoogleFonts.montserrat(
                              fontSize: 15, fontStyle: FontStyle.italic)),
                    )
                  : Scrollbar(
                      thumbVisibility: true,
                      controller: contextScroll,
                      child: SingleChildScrollView(
                        controller: contextScroll,
                        child: Padding(
                          padding: const EdgeInsets.only(right: 15),
                          child: Text(
                            textAlign: TextAlign.justify,
                            quest.context!,
                            style: GoogleFonts.openSans(fontSize: 15),
                          ),
                        ),
                      ),
                    ),
            ),
          ],
        ),
      ),
    );
  }
}
