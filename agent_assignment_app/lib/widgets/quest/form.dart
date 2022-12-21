import 'package:agent_assignment_app/helpers/string.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:agent_assignment_app/services/apis/quest.dart';
import 'package:agent_assignment_app/services/providers/category.dart';
import 'package:agent_assignment_app/widgets/quest/detail/screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:loader_overlay/loader_overlay.dart';
import 'package:provider/provider.dart';
import 'package:reactive_forms/reactive_forms.dart';

class QuestForm extends StatefulWidget {
  final Quest? questEdit;
  final VoidCallback refresher;
  const QuestForm({super.key, required this.refresher, this.questEdit});

  @override
  State<QuestForm> createState() => _QuestFormState();
}

class _QuestFormState extends State<QuestForm> {
  bool get isEditMode {
    return widget.questEdit != null;
  }

  late final FormGroup form;
  late final String? questId;

  @override
  void initState() {
    final quest = widget.questEdit;
    questId = quest?.id;
    form = fb.group(<String, Object>{
      'context': isEditMode ? quest!.context ?? '' : '',
      'questStatus': fb.control<int>(
          isEditMode ? quest!.questStatus : QuestStatus.waiting.index,
          [Validators.required]),
      'necessity': fb.control<int>(
          isEditMode ? quest!.necessity : QuestNecessity.asap.index,
          [Validators.required]),
      'categoryId': [isEditMode ? quest!.categoryId : '', Validators.required],
      'expired': isEditMode ? quest!.expired ?? '' : DateTime.now(),
      'numberOfAgent': [
        isEditMode ? quest!.numberOfAgent : 1,
        Validators.required,
        _requiredAtLeastOne
      ]
    });
    super.initState();
  }

  Map<String, dynamic>? _requiredAtLeastOne(AbstractControl<dynamic> control) {
    return control.isNotNull && control.value is int && control.value > 0
        ? null
        : {'atLeastOne': true};
  }

  @override
  void dispose() {
    form.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final categories = Provider.of<CateogryProvider>(context).categories;
    return _buildMainWrapper(context,
        child: ReactiveFormBuilder(
          form: () => form,
          builder: (context, form, child) {
            return Column(
                crossAxisAlignment: CrossAxisAlignment.end,
                children: [
                  ReactiveDropdownField<int>(
                    decoration:
                        _buildDecoration('Status', Icons.work).copyWith(),
                    dropdownColor: const Color.fromRGBO(39, 50, 58, 1),
                    formControlName: 'questStatus',
                    items: QuestStatus.values
                        .map((e) => DropdownMenuItem(
                              value: e.index,
                              child: Text(
                                e.getDisplayStatus(),
                                style: TextStyle(
                                    color: e.getColorStatus(),
                                    fontWeight: FontWeight.bold),
                              ),
                            ))
                        .where((element) =>
                            element.value != QuestStatus.created.index)
                        .toList(),
                  ),
                  const SizedBox(
                    height: 20,
                  ),
                  ReactiveDropdownField<int>(
                    decoration:
                        _buildDecoration('Necessity', Icons.hourglass_empty)
                            .copyWith(),
                    dropdownColor: const Color.fromRGBO(39, 50, 58, 1),
                    formControlName: 'necessity',
                    items: QuestNecessity.values
                        .map((e) => DropdownMenuItem(
                              value: e.index,
                              child: Text(
                                e.getDisplayStatus(),
                                style: const TextStyle(
                                    fontWeight: FontWeight.bold),
                              ),
                            ))
                        .toList(),
                  ),
                  const SizedBox(
                    height: 20,
                  ),
                  Opacity(
                    opacity: categories == null || categories.isEmpty ? 0.3 : 1,
                    child: ReactiveDropdownField<String>(
                      decoration: _buildDecoration('Quest category', Icons.work)
                          .copyWith(),
                      formControlName: 'categoryId',
                      dropdownColor: const Color.fromRGBO(39, 50, 58, 1),
                      items: categories == null
                          ? []
                          : categories
                              .map((e) => DropdownMenuItem<String>(
                                    value: e.id,
                                    child: Text(
                                      displayFromSnakeStyle(e.name),
                                    ),
                                  ))
                              .toList(),
                      validationMessages: {
                        'required': (error) => 'The quest must has a category'
                      },
                    ),
                  ),
                  const SizedBox(
                    height: 20,
                  ),
                  ReactiveTextField(
                    minLines: 3,
                    maxLines: 7,
                    decoration: _buildDecoration('Context', Icons.info),
                    formControlName: 'context',
                  ),
                  const SizedBox(
                    height: 20,
                  ),
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Expanded(
                        child: InkWell(
                          onTap: () async {
                            final selectedDate = await showDatePicker(
                                context: context,
                                initialDate: DateTime.now(),
                                firstDate: DateTime(2022),
                                lastDate: DateTime(2023));
                            form.control('expired').value = selectedDate;
                          },
                          child: IgnorePointer(
                            child: ReactiveTextField(
                              decoration:
                                  _buildDecoration('Expired', Icons.date_range),
                              formControlName: 'expired',
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(
                        width: 20,
                      ),
                      Expanded(
                        child: ReactiveTextField<int>(
                          keyboardType: TextInputType.number,
                          decoration:
                              _buildDecoration('Number of agents', Icons.info),
                          formControlName: 'numberOfAgent',
                          inputFormatters: [
                            FilteringTextInputFormatter.allow(
                                RegExp(r"[0-9.,]")),
                            TextInputFormatter.withFunction(
                                (oldValue, newValue) {
                              try {
                                final text = newValue.text;
                                if (text.isNotEmpty) double.parse(text);
                                return newValue;
                              } catch (e) {
                                return oldValue;
                              }
                            })
                          ],
                          validationMessages: {
                            'required': (error) =>
                                'The quest must has a number of agent',
                            'atLeastOne': (error) =>
                                'The quest must has at least 1 agent',
                          },
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(
                    height: 20,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      _buildButton('Cancel', Colors.red,
                          onPressed: () => Navigator.of(context).pop()),
                      const SizedBox(
                        width: 20,
                      ),
                      ReactiveFormConsumer(
                          builder: (buildContext, form, child) {
                        if (form.valid) {
                          return isEditMode
                              ? _buildButton('Save', Colors.yellow.shade800,
                                  onPressed: () {
                                  if (form.valid) {
                                    context.loaderOverlay.show();
                                    QuestAPI.editQuest(questId!, form.rawValue)
                                        .then((_) {
                                      context.loaderOverlay.hide();
                                      Navigator.pop(context);
                                      Navigator.pop(context);
                                      widget.refresher();
                                      Navigator.of(context).pushNamed(
                                          QuestDetailScreen.route,
                                          arguments: {
                                            'questId': questId,
                                            'refresher': widget.refresher
                                          });
                                    });
                                  }
                                })
                              : _buildButton('Add Quest', Colors.green,
                                  onPressed: () {
                                  if (form.valid) {
                                    context.loaderOverlay.show();
                                    QuestAPI.addQuest(form.rawValue)
                                        .then((value) {
                                      Navigator.pop(context);
                                      context.loaderOverlay.hide();
                                      widget.refresher();
                                      Navigator.of(context).pushNamed(
                                          QuestDetailScreen.route,
                                          arguments: {
                                            'questId': value,
                                            'refresher': widget.refresher
                                          });
                                    });
                                  }
                                });
                        } else {
                          return _buildButton('Add Quest', Colors.green);
                        }
                      }),
                    ],
                  ),
                ]);
          },
        ));
  }

  InputDecoration _buildDecoration(String labelText, IconData icon) {
    return InputDecoration(
      border: const OutlineInputBorder(),
      enabledBorder: const OutlineInputBorder(
        borderSide: BorderSide(color: Colors.white),
      ),
      labelStyle: const TextStyle(color: Colors.white),
      labelText: labelText,
      prefixIcon: Icon(
        icon,
        color: Colors.white,
      ),
      fillColor: Colors.white,
      focusedBorder: const OutlineInputBorder(
        borderSide: BorderSide(color: Colors.white, width: 2.0),
      ),
    );
  }

  Widget _buildMainWrapper(BuildContext context, {required Widget child}) {
    return LoaderOverlay(
      useDefaultLoading: false,
      overlayWidget: const Center(
        child: SpinKitCubeGrid(
          color: Colors.lightBlue,
          size: 50.0,
        ),
      ),
      child: Container(
        height: double.infinity,
        padding: EdgeInsets.symmetric(
            horizontal: MediaQuery.of(context).size.width * 0.2, vertical: 30),
        decoration: const BoxDecoration(
            gradient: LinearGradient(colors: [
          Color.fromRGBO(20, 30, 48, 1),
          Color.fromRGBO(36, 59, 85, 1)
        ])),
        child: child,
      ),
    );
  }

  Widget _buildButton(String text, Color color, {VoidCallback? onPressed}) {
    return ElevatedButton(
      onPressed:
          onPressed, // This child can be everything. I want to choose a beautiful Text Widget
      style: ElevatedButton.styleFrom(
        foregroundColor: Colors.white,
        textStyle: const TextStyle(fontSize: 15, fontWeight: FontWeight.bold),
        disabledForegroundColor: color.withOpacity(0.8),
        disabledBackgroundColor: color.withOpacity(0.12),
        disabledMouseCursor: SystemMouseCursors.forbidden,
        backgroundColor: color,
        fixedSize: const Size(120, 50),
        // surface color
        shadowColor: Colors
            .grey, //shadow prop is a very nice prop for every button or card widgets.
        elevation: 5, // we can set elevation of this beautiful button
        side: BorderSide(
            color: color..withOpacity(0.4), //change border color
            width: 2, //change border width
            style: BorderStyle
                .solid), // change border side of this beautiful button
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(
              30), //change border radius of this beautiful button thanks to BorderRadius.circular function
        ),
        tapTargetSize: MaterialTapTargetSize.padded,
      ), //This prop for beautiful expressions
      child: Text(text),
    );
  }
}
