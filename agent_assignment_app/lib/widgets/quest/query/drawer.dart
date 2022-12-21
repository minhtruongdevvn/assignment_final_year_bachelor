import 'package:agent_assignment_app/helpers/String.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/providers/category.dart';
import 'package:agent_assignment_app/widgets/quest/query/button.dart';
import 'package:agent_assignment_app/widgets/quest/query/form_item.dart';
import 'package:agent_assignment_app/widgets/quest/query/switch.dart';
import 'package:dropdown_textfield/dropdown_textfield.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

class QueryDrawer extends StatefulWidget {
  static const dateMode = ['DateCreated', 'Expired'];
  static const sortMode = ['Ascending', 'Descending'];
  final Map<String, dynamic> cachedField;

  const QueryDrawer({Key? key, required this.cachedField}) : super(key: key);

  @override
  State<QueryDrawer> createState() => _QueryDrawerState();
}

class _QueryDrawerState extends State<QueryDrawer> {
  final sortOptions = [
    {'name': 'Code', 'value': 'Code'},
    {'name': 'Created date', 'value': QueryDrawer.dateMode[0]},
    {'name': 'Expired date', 'value': QueryDrawer.dateMode[1]},
    {'name': 'Number Of Agent', 'value': 'NumberOfAgent'}
  ];
  var codeController = TextEditingController();
  var noAgentController = TextEditingController();
  var statusController = SingleValueDropDownController();
  var fromDateController = TextEditingController();
  var toDateController = TextEditingController();
  var categoryController = SingleValueDropDownController();
  var sortController = SingleValueDropDownController();
  String sortMode = '';
  String dateMode = '';

  @override
  void dispose() {
    codeController.dispose();
    noAgentController.dispose();
    fromDateController.dispose();
    toDateController.dispose();
    categoryController.dispose();
    statusController.dispose();
    sortController.dispose();

    super.dispose();
  }

  @override
  void initState() {
    super.initState();
    sortMode = widget.cachedField['sortMode']!;
    dateMode = widget.cachedField['dateMode']!;
    codeController.text = widget.cachedField['code'] ?? '';
    noAgentController.text = widget.cachedField['numberOfAgent'] ?? '';
    fromDateController.text = widget.cachedField['fromDate'] ?? '';
    toDateController.text = widget.cachedField['toDate'] ?? '';

    categoryController.dropDownValue = widget.cachedField['categoryId'] == null
        ? null
        : DropDownValueModel(
            name: widget.cachedField['categoryName']!,
            value: widget.cachedField['categoryId']);

    final sortOption = widget.cachedField['sort'] == null
        ? null
        : sortOptions.firstWhere((option) =>
            option.values.any((e) => e == widget.cachedField['sort']));

    sortController.dropDownValue = sortOption == null
        ? null
        : DropDownValueModel(
            name: sortOption['name']!, value: sortOption['value']!);

    final status = widget.cachedField['status'] == null
        ? null
        : QuestStatus.values[int.parse(widget.cachedField['status']!)];

    statusController.dropDownValue = status == null
        ? null
        : DropDownValueModel(
            name: status.getDisplayStatus(), value: status.index.toString());
  }

  void _handleChange() {
    Map<String, dynamic> newField = {
      'code': codeController.text,
      'numberOfAgent': noAgentController.text,
      'status': statusController.dropDownValue?.value,
      'fromDate': fromDateController.text,
      'toDate': toDateController.text,
      'categoryId': categoryController.dropDownValue?.value,
      'categoryName': categoryController.dropDownValue?.name,
      'sort': sortController.dropDownValue?.value,
      'sortMode': sortMode,
      'dateMode': dateMode
    };

    for (final item in widget.cachedField.entries) {
      newField.putIfAbsent(item.key, () => item.value);
    }

    Navigator.of(context).pop(newField);
  }

  void _handleClear() {
    codeController.clear();
    noAgentController.clear();
    fromDateController.clear();
    toDateController.clear();

    setState(() {
      sortMode = QueryDrawer.sortMode[0];
      dateMode = QueryDrawer.dateMode[0];
      categoryController.clearDropDown();
      statusController.clearDropDown();
      sortController.clearDropDown();
    });
  }

  List<Map<String, String?>> mapCategoriesToOptions(BuildContext context) {
    final categories = Provider.of<CateogryProvider>(context).categories;
    if (categories == null || categories.isEmpty) {
      return List<Map<String, String>>.empty();
    }
    return categories
        .map((e) => {'name': displayFromSnakeStyle(e.name), 'value': e.id})
        .toList();
  }

  @override
  Widget build(BuildContext context) {
    final options = mapCategoriesToOptions(context);
    return _buildMainWrapper(childrens: [
      Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          Row(
            children: [
              Expanded(
                child: QueryFormItem(
                  controller: codeController,
                  hintText: 'Filter by code',
                ),
              ),
              Expanded(
                child: QueryFormItem(
                  inputFormatters: [
                    FilteringTextInputFormatter.allow(RegExp(r"[0-9.,]")),
                    TextInputFormatter.withFunction((oldValue, newValue) {
                      try {
                        final text = newValue.text;
                        if (text.isNotEmpty) double.parse(text);
                        return newValue;
                      } catch (e) {
                        return oldValue;
                      }
                    })
                  ],
                  controller: noAgentController,
                  hintText: 'Number of agent',
                ),
              ),
            ],
          ),
          QueryFormItem(
              icon: Icons.category,
              disable: options.isEmpty,
              controller: categoryController,
              hintText: 'Filter By Category',
              dropdownList: options),
          QueryFormItem(
              icon: Icons.work,
              controller: statusController,
              hintText: 'Filter By Status',
              dropdownList: QuestStatus.values
                  .map((e) => {
                        'name': e.getDisplayStatus(),
                        'value': e.index.toString()
                      })
                  .toList()),
          QuerySwitch(
            initialLabelIndex:
                QueryDrawer.dateMode.indexWhere((e) => e == dateMode),
            title: 'Date mode',
            minWidth: 100,
            labels: const ['Created', 'Expired'],
            iconData: const [Icons.add, Icons.close],
            onChanged: (value) => dateMode = QueryDrawer.dateMode[value],
          ),
          Row(
            children: [
              Expanded(
                  child: QueryFormItem(
                      icon: Icons.date_range,
                      controller: fromDateController,
                      hintText: 'From Date',
                      onTap: () => _selectDate(fromDateController))),
              Expanded(
                  child: QueryFormItem(
                      icon: Icons.date_range,
                      controller: toDateController,
                      hintText: 'To date',
                      onTap: () => _selectDate(toDateController)))
            ],
          ),
          QuerySwitch(
              initialLabelIndex:
                  QueryDrawer.sortMode.indexWhere((e) => e == sortMode),
              title: 'Sort mode',
              minWidth: 120,
              labels: QueryDrawer.sortMode,
              iconData: const [Icons.arrow_upward, Icons.arrow_downward],
              onChanged: (value) => sortMode = QueryDrawer.sortMode[value]),
          QueryFormItem(
              icon: Icons.sort,
              controller: sortController,
              hintText: 'Sort By Field',
              dropdownList: sortOptions),
          SizedBox(
            width: 150,
            child:
                QueryButton(name: 'Clear', onTap: _handleClear, colors: const [
              Color.fromRGBO(195, 20, 50, 1),
              Color.fromRGBO(36, 11, 54, 1),
            ]),
          )
        ],
      ),
      const SizedBox(
        height: 80,
      ),
      QueryButton(name: 'Click here to confirm', onTap: _handleChange)
    ]);
  }

  Future _selectDate(TextEditingController controller) async {
    DateTime? picked = await showDatePicker(
        context: context,
        initialDate: DateTime.now(),
        firstDate: DateTime(2022),
        lastDate: DateTime(2023));

    controller.text =
        picked == null ? '' : DateFormat('yyyy-MM-dd').format(picked);
  }

  Widget _buildMainWrapper({required List<Widget> childrens}) {
    return Container(
      padding: const EdgeInsets.only(left: 15, right: 15, top: 45, bottom: 45),
      height: double.infinity,
      width: double.infinity,
      decoration: const BoxDecoration(
          borderRadius: BorderRadius.only(
              topLeft: Radius.circular(30), bottomLeft: Radius.circular(30)),
          gradient: LinearGradient(colors: [
            Color.fromRGBO(43, 88, 118, 1),
            Color.fromRGBO(78, 67, 118, 1)
          ])),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: childrens,
      ),
    );
  }
}
