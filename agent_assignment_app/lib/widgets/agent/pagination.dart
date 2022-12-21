import 'dart:async';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

// ignore: must_be_immutable
class Pagination extends StatelessWidget {
  final int currentPage;
  final int numberPages;
  final ValueSetter<int> onPageChange;
  Timer? _debounce;
  final TextEditingController controller = TextEditingController();

  Pagination({
    Key? key,
    required this.currentPage,
    required this.numberPages,
    required this.onPageChange,
  }) : super(key: key);

  bool get isLastPage {
    return currentPage >= numberPages;
  }

  bool get isFirstPage {
    return currentPage <= 1;
  }

  @override
  Widget build(BuildContext context) {
    int visibleNumberOfElement = 7;
    return FittedBox(
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisSize: MainAxisSize.min,
        children: [
          const Text("Jump to page:",
              style: TextStyle(
                  color: Color.fromRGBO(132, 133, 140, 1),
                  fontWeight: FontWeight.bold)),
          const SizedBox(
            width: 20,
          ),
          SizedBox(
            width: 50,
            height: 25,
            child: TextField(
              controller: controller,
              onChanged: (value) {
                if (_debounce?.isActive ?? false) _debounce!.cancel();
                _debounce = Timer(const Duration(milliseconds: 1200), () {
                  final page = int.tryParse(value);
                  if (page == null) return;
                  controller.clear();
                  onPageChange(page);
                });
              },
              inputFormatters: [
                FilteringTextInputFormatter.allow(RegExp(r"[0-9.,]")),
                TextInputFormatter.withFunction((oldValue, newValue) {
                  try {
                    final text = newValue.text;
                    var value = 0;
                    if (text.isNotEmpty) value = int.parse(text);
                    if (value > numberPages) {
                      return oldValue;
                    }
                    return newValue;
                  } catch (e) {
                    return oldValue;
                  }
                })
              ],
              textAlignVertical: TextAlignVertical.center,
              decoration: InputDecoration(
                filled: true,
                fillColor: const Color.fromRGBO(59, 60, 69, 1),
                focusedBorder: _buildOutlineInput(),
                border: _buildOutlineInput(),
                enabledBorder: _buildOutlineInput(),
                contentPadding: const EdgeInsets.symmetric(horizontal: 10),
              ),
            ),
          ),
          const SizedBox(
            width: 30,
          ),
          TextButton(
              onPressed:
                  isFirstPage ? null : () => onPageChange(currentPage - 1),
              child: Icon(
                FontAwesomeIcons.chevronLeft,
                size: 12.4,
                color: Color.fromRGBO(132, 133, 140, isFirstPage ? 0.3 : 1),
              )),
          _buildPageButton(context, 0),
          if (_frontDotsShouldShow(context, visibleNumberOfElement))
            _buildDots(context),
          if (numberPages > 1)
            ..._generateButtonList(context, visibleNumberOfElement),
          if (_backDotsShouldShow(context, visibleNumberOfElement))
            _buildDots(context),
          if (numberPages > 1) _buildPageButton(context, numberPages - 1),
          TextButton(
              onPressed:
                  isLastPage ? null : () => onPageChange(currentPage + 1),
              child: Icon(
                FontAwesomeIcons.chevronRight,
                size: 12.4,
                color: Color.fromRGBO(132, 133, 140, isLastPage ? 0.3 : 1),
              )),
        ],
      ),
    );
  }

  OutlineInputBorder _buildOutlineInput() {
    return const OutlineInputBorder(
        borderRadius: BorderRadius.all(Radius.circular(3.0)),
        borderSide: BorderSide(color: Color.fromRGBO(59, 60, 69, 1)));
  }

  /// Generates the variable button list which is at the center of the (optional)
  /// dots. The very last and first pages are shown independently of this list.
  List<Widget> _generateButtonList(BuildContext context, int availableSpots) {
    // if dots shown: available minus (2 for first and last pages + 2 for dots)
    var shownPages = availableSpots -
        2 -
        (_backDotsShouldShow(context, availableSpots) ? 1 : 0) -
        (_frontDotsShouldShow(context, availableSpots) ? 1 : 0);

    int minValue, maxValue;
    minValue = max(1, currentPage - shownPages ~/ 2);
    maxValue = min(minValue + shownPages, numberPages - 1);
    if (maxValue - minValue < shownPages) {
      minValue = (maxValue - shownPages).clamp(1, numberPages - 1);
    }

    return List.generate(maxValue - minValue,
        (index) => _buildPageButton(context, minValue + index));
  }

  /// Builds a button for the given index.
  Widget _buildPageButton(BuildContext context, int index) => SizedBox(
        width: 30,
        child: TextButton(
          onPressed: () => onPageChange(index + 1),
          child: Text(
            (index + 1).toString(),
            style: TextStyle(
                color: index + 1 == currentPage
                    ? const Color.fromRGBO(208, 240, 225, 1)
                    : const Color.fromRGBO(132, 133, 140, 1)),
          ),
        ),
      );

  Widget _buildDots(BuildContext context) => SizedBox(
        width: 40,
        child: TextButton(
          onPressed: () {},
          child: const Text("...",
              style: TextStyle(color: Color.fromRGBO(132, 133, 140, 1))),
        ),
      );

  /// Checks if pages don't fit in available spots and dots have to be shown.
  bool _backDotsShouldShow(BuildContext context, int availableSpots) =>
      availableSpots < numberPages &&
      currentPage < numberPages - availableSpots ~/ 2;

  bool _frontDotsShouldShow(BuildContext context, int availableSpots) =>
      availableSpots < numberPages && currentPage > availableSpots ~/ 2 - 1;
}
