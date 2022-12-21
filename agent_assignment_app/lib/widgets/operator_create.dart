import 'package:agent_assignment_app/services/apis/operator.dart';
import 'package:agent_assignment_app/widgets/common_screen.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:loader_overlay/loader_overlay.dart';
import 'package:reactive_forms/reactive_forms.dart';

class OperatorCreate extends StatefulWidget {
  static const String route = 'operator/create';
  const OperatorCreate({super.key});

  @override
  State<OperatorCreate> createState() => _OperatorCreateState();
}

class _OperatorCreateState extends State<OperatorCreate> {
  late final FormGroup form;
  late final String? questId;
  final Map<String, String Function(Object)> errorMessages = {
    'required': (error) => 'This field is required',
    'email': (error) => 'Email must be valid',
    'mustMatch': (error) => 'Confirm password is not correct'
  };

  @override
  void initState() {
    form = FormGroup({
      'email': FormControl<String>(
          validators: [Validators.required, Validators.email]),
      'password': FormControl<String>(validators: [
        Validators.required,
      ]),
      'passwordConfirmation': FormControl<String>(validators: [
        Validators.required,
      ]),
      'familyName': FormControl<String>(validators: [
        Validators.required,
      ]),
      'givenName': FormControl<String>(validators: [
        Validators.required,
      ]),
      'internalCode': FormControl<String>(validators: [
        Validators.required,
      ]),
    }, validators: [
      Validators.mustMatch('password', 'passwordConfirmation'),
    ]);

    super.initState();
  }

  @override
  void dispose() {
    form.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return _buildMainWrapper(context,
        child: ReactiveFormBuilder(
          form: () => form,
          builder: (context, form, child) {
            return SingleChildScrollView(
              child:
                  Column(crossAxisAlignment: CrossAxisAlignment.end, children: [
                ReactiveTextField(
                  decoration: _buildDecoration('Email ', Icons.info),
                  formControlName: 'email',
                  validationMessages: errorMessages,
                ),
                const SizedBox(
                  height: 20,
                ),
                ReactiveTextField(
                  decoration: _buildDecoration('Password', Icons.info),
                  obscureText: true,
                  formControlName: 'password',
                  validationMessages: errorMessages,
                ),
                const SizedBox(
                  height: 20,
                ),
                ReactiveTextField(
                  decoration: _buildDecoration('Confirm password', Icons.info),
                  obscureText: true,
                  formControlName: 'passwordConfirmation',
                  validationMessages: errorMessages,
                ),
                const SizedBox(
                  height: 20,
                ),
                ReactiveTextField(
                  decoration: _buildDecoration('Family name', Icons.info),
                  formControlName: 'familyName',
                  validationMessages: errorMessages,
                ),
                const SizedBox(
                  height: 20,
                ),
                ReactiveTextField(
                  decoration: _buildDecoration('Given name', Icons.info),
                  formControlName: 'givenName',
                  validationMessages: errorMessages,
                ),
                const SizedBox(
                  height: 20,
                ),
                ReactiveTextField(
                  decoration: _buildDecoration('Internal code', Icons.info),
                  formControlName: 'internalCode',
                  validationMessages: errorMessages,
                ),
                const SizedBox(
                  height: 20,
                ),
                ReactiveFormConsumer(
                  builder: (context, form, child) {
                    return _buildButton('Add Operator', Colors.green,
                        onPressed: form.invalid
                            ? null
                            : () {
                                context.loaderOverlay.show();
                                OperatorAPI.addOperator(form.rawValue)
                                    .then((_) {
                                  form.reset();
                                  context.loaderOverlay.hide();
                                });
                              });
                  },
                ),
              ]),
            );
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
    return CommonScreen(
      child: Container(
        margin: const EdgeInsets.all(20),
        height: double.infinity,
        padding: EdgeInsets.symmetric(
            horizontal: MediaQuery.of(context).size.width * 0.2, vertical: 30),
        decoration: const BoxDecoration(
            borderRadius: BorderRadius.all(Radius.circular(8)),
            gradient: LinearGradient(colors: [
              Color.fromRGBO(20, 30, 48, 1),
              Color.fromRGBO(36, 59, 85, 1)
            ])),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text("Add Operator", style: GoogleFonts.adamina(fontSize: 35)),
            const SizedBox(
              height: 40,
            ),
            child,
          ],
        ),
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
        fixedSize: const Size(150, 50),
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
