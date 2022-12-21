import 'package:agent_assignment_app/models/quest.dart';
import 'package:flutter/material.dart';
import 'package:flutter_expandable_fab/flutter_expandable_fab.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:just_the_tooltip/just_the_tooltip.dart';

JustTheTooltip buildTooltipWrapper(String tooltip, Widget child,
    {AxisDirection direction = AxisDirection.up}) {
  return JustTheTooltip(
    preferredDirection: direction,
    offset: 5,
    tailLength: 10,
    tailBaseWidth: 10,
    backgroundColor: const Color.fromRGBO(66, 64, 66, 1),
    content: Padding(
      padding: const EdgeInsets.all(8.0),
      child: Text(tooltip),
    ),
    child: child,
  );
}

Widget buildCommonFAB(
    {required List<Widget> children,
    ExpandableFabType type = ExpandableFabType.left,
    EdgeInsetsGeometry margin = const EdgeInsets.only(bottom: 10, right: 10)}) {
  return Container(
    margin: margin,
    child: ExpandableFab(
      closeButtonStyle: const ExpandableFabCloseButtonStyle(
          backgroundColor: Colors.redAccent),
      childrenOffset: const Offset(10, 8),
      type: type,
      distance: type == ExpandableFabType.left ? 50 : 75,
      children: children,
    ),
  );
}

Widget buildDefaultWrapper<TDefault, TCurrent>(
    TDefault defaultObject, TDefault currentObject,
    {required Widget child}) {
  if (defaultObject == currentObject) {
    return Stack(
      children: const [
        Opacity(
          opacity: 0.2,
          child: ModalBarrier(dismissible: false, color: Colors.white),
        ),
        Center(
          child: SpinKitCubeGrid(
            color: Colors.lightBlue,
            size: 50.0,
          ),
        ),
      ],
    );
  } else {
    return child;
  }
}

Widget buildNullWrapper<TObject>(TObject? object,
    {required Widget Function(TObject value) builder,
    String message = 'No content'}) {
  if (object == null) {
    return Center(
      child: Text(message,
          style: GoogleFonts.montserrat(
              fontSize: 15, fontStyle: FontStyle.italic)),
    );
  }

  return builder(object);
}

Widget buildQuestLockWrapper(Quest defaultQuest, Quest currentQuest,
    {required Widget child}) {
  return buildDefaultWrapper(defaultQuest, currentQuest,
      child: currentQuest.questStatus == QuestStatus.failed.index ||
              currentQuest.questStatus == QuestStatus.success.index
          ? Center(
              child: Text('This quest is complete',
                  style: GoogleFonts.montserrat(
                      fontSize: 15, fontStyle: FontStyle.italic)),
            )
          : child);
}

Widget buildEmptyWrapper<TEmptyable>(List<TEmptyable> collection,
    {required Widget child, String message = 'Empty'}) {
  return collection.isEmpty
      ? Center(
          child: Text(message,
              style: GoogleFonts.montserrat(
                  fontSize: 15, fontStyle: FontStyle.italic)),
        )
      : child;
}
