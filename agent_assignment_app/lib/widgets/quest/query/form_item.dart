import 'package:dropdown_textfield/dropdown_textfield.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class QueryFormItem extends StatelessWidget {
  final String hintText;
  final int maxLines;
  final int minLines;
  final bool readOnly;
  final List<Map<String, String?>>? dropdownList;
  final VoidCallback? onTap;
  final bool disable;
  final IconData icon;
  final List<TextInputFormatter>? inputFormatters;

  ///It must be type of SingleValueDropDownController for dropdown or TextEditingController for textfield.
  final dynamic controller;

  const QueryFormItem(
      {Key? key,
      required this.hintText,
      required this.controller,
      this.maxLines = 1,
      this.minLines = 1,
      this.readOnly = false,
      this.dropdownList,
      this.onTap,
      this.disable = false,
      this.icon = Icons.content_copy,
      this.inputFormatters})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return dropdownList != null
        ? _dropdownChild(context)
        : InkWell(
            onTap: onTap,
            child: onTap == null
                ? _textfieldChild()
                : IgnorePointer(
                    child: _textfieldChild(),
                  ),
          );
  }

  InputDecoration _decoration(IconData iconData) {
    return InputDecoration(
        prefixIconConstraints: const BoxConstraints(minWidth: 45),
        prefixIcon: Icon(
          iconData,
          color: disable ? Colors.white.withOpacity(0.3) : Colors.white,
          size: 22,
        ),
        border: InputBorder.none,
        hintText: hintText,
        hintStyle: const TextStyle(color: Colors.white60, fontSize: 14.5),
        disabledBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(100)
                .copyWith(bottomRight: const Radius.circular(0)),
            borderSide: const BorderSide(color: Colors.blueGrey)),
        enabledBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(100)
                .copyWith(bottomRight: const Radius.circular(0)),
            borderSide: const BorderSide(color: Colors.white38)),
        focusedBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(100)
                .copyWith(bottomRight: const Radius.circular(0)),
            borderSide: const BorderSide(color: Colors.white70)));
  }

  Widget _textfieldChild() {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 10).copyWith(bottom: 10),
      child: TextField(
        inputFormatters: inputFormatters,
        enabled: !disable,
        controller: controller,
        readOnly: readOnly,
        minLines: minLines,
        maxLines: maxLines,
        style: TextStyle(
            color: disable ? Colors.blueGrey : Colors.white, fontSize: 14.5),
        decoration: _decoration(icon),
      ),
    );
  }

  Widget _dropdownChild(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 10).copyWith(bottom: 10),
      child: DropDownTextField(
          isEnabled: !disable,
          controller: controller,
          dropDownItemCount: 7,
          searchDecoration:
              const InputDecoration(fillColor: Color.fromRGBO(0, 71, 171, 1)),
          textFieldDecoration: _decoration(icon),
          textStyle: Theme.of(context)
              .textTheme
              .bodyMedium!
              .copyWith(color: disable ? Colors.grey : Colors.white),
          listTextStyle: const TextStyle(color: Colors.black),
          dropDownIconProperty: IconProperty(
              color: disable ? Colors.grey.withOpacity(0.3) : Colors.white,
              size: 30),
          dropDownList: dropdownList!
              .map((e) =>
                  DropDownValueModel(name: e['name']!, value: e['value']!))
              .toList()),
    );
  }
}
