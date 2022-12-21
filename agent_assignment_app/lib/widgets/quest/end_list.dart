import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class EndOfList extends StatefulWidget {
  final ScrollController controller;
  const EndOfList({super.key, required this.controller});

  @override
  State<EndOfList> createState() => _EndOfListState();
}

class _EndOfListState extends State<EndOfList> {
  bool isBottom = false;
  @override
  void initState() {
    super.initState();

    widget.controller.addListener(() {
      // reached bottom
      if (widget.controller.offset >=
          widget.controller.position.maxScrollExtent) {
        setState(() => isBottom = true);
      } else {
        setState(() => isBottom = false);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: isBottom
          ? Text(
              'You\'ve reached the end',
              style: GoogleFonts.adamina(
                  fontSize: 17,
                  color: Colors.grey,
                  fontStyle: FontStyle.italic),
            )
          : Container(),
    );
  }
}
