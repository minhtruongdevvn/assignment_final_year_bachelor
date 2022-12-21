import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:toggle_switch/toggle_switch.dart';

class QuerySwitch extends StatefulWidget {
  final String title;
  final MaterialColor color;
  final double minWidth;
  final List<IconData> iconData;
  final List<String> labels;
  final Function(int value) onChanged;
  final int initialLabelIndex;
  const QuerySwitch(
      {super.key,
      this.color = Colors.red,
      required this.onChanged,
      required this.iconData,
      required this.labels,
      this.minWidth = 90,
      required this.title,
      required this.initialLabelIndex});

  @override
  State<QuerySwitch> createState() => _QuerySwitchState();
}

class _QuerySwitchState extends State<QuerySwitch> {
  bool value = false;
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 25)
          .copyWith(bottom: 10, top: 20),
      child: Row(
        children: [
          Text(
            '${widget.title}: ',
            style: GoogleFonts.acme(),
          ),
          ToggleSwitch(
            minHeight: 30,
            minWidth: widget.minWidth,
            initialLabelIndex: widget.initialLabelIndex,
            cornerRadius: 20.0,
            activeFgColor: Colors.white,
            inactiveBgColor: Colors.grey,
            inactiveFgColor: Colors.white,
            totalSwitches: 2,
            labels: widget.labels,
            icons: widget.iconData,
            activeBgColors: const [
              [Colors.blue],
              [Colors.blue]
            ],
            onToggle: (index) {
              widget.onChanged(index!);
            },
          ),
        ],
      ),
    );
  }
}
