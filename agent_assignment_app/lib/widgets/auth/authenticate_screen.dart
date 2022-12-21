import 'dart:io';

import 'package:agent_assignment_app/services/providers/auth.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:just_the_tooltip/just_the_tooltip.dart';
import 'package:lottie/lottie.dart';
import 'package:provider/provider.dart';
import 'package:window_manager/window_manager.dart';

class AuthenticateScreen extends StatefulWidget {
  const AuthenticateScreen({Key? key}) : super(key: key);

  @override
  State<AuthenticateScreen> createState() => _AuthenticateScreenState();
}

class _AuthenticateScreenState extends State<AuthenticateScreen> {
  bool _isLoading = false;
  TextEditingController nameController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  bool _showPassword = false;
  final _formKey = GlobalKey<FormState>();
  void _togglePasswordVisible() {
    setState(() {
      _showPassword = !_showPassword;
    });
  }

  void _showErrorDialog(String message) {
    showDialog(
      context: context,
      builder: (ctx) => AlertDialog(
        backgroundColor: const Color.fromRGBO(4, 13, 33, 1),
        title: const Text('An Error Occurred!'),
        content: Text(
          message,
        ),
        actions: <Widget>[
          TextButton(
            child: const Text(
              'Okay',
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 15),
            ),
            onPressed: () {
              Navigator.of(ctx).pop();
            },
          )
        ],
      ),
    );
  }

  Future<void> _handleSubmit() async {
    if (!_formKey.currentState!.validate()) {
      // Invalid!
      return;
    }
    setState(() {
      _isLoading = true;
    });
    try {
      var isSuccess = await Provider.of<Auth>(context, listen: false).login(
        nameController.text,
        passwordController.text,
      );
      if (!isSuccess) _showErrorDialog('Permission denied');
    } on HttpException catch (error) {
      var errorMessage = 'Authentication failed';
      if (error.toString().contains('invalid_username_or_password')) {
        errorMessage = 'Username or password is wrong.';
      }
      _showErrorDialog(errorMessage);
    } catch (error) {
      const errorMessage =
          'Could not authenticate you. Please try again later.';
      _showErrorDialog(errorMessage);
    }
    setState(() {
      _isLoading = false;
    });
  }

  @override
  void dispose() {
    nameController.dispose();
    passwordController.dispose();
    super.dispose();
  }

  Widget _buildMainBody(Size size) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        SizedBox(
          height: size.height * 0.03,
        ),
        Padding(
          padding: const EdgeInsets.only(left: 20.0),
          child: Text(
            'Login',
            style: GoogleFonts.lato(
              fontSize: size.height * 0.060,
              fontWeight: FontWeight.bold,
            ),
          ),
        ),
        const SizedBox(
          height: 10,
        ),
        Padding(
          padding: const EdgeInsets.only(left: 20.0),
          child: Text(
            'Welcome Back Manager',
            style: GoogleFonts.lato(
              fontSize: size.height * 0.030,
            ),
          ),
        ),
        SizedBox(
          height: size.height * 0.03,
        ),
        Padding(
          padding: const EdgeInsets.only(left: 20.0, right: 20),
          child: Form(
            key: _formKey,
            child: Column(
              children: [
                TextFormField(
                  enabled: !_isLoading,
                  style: const TextStyle(
                    color: Colors.lightBlue,
                  ),
                  cursorColor: Colors.deepPurpleAccent,
                  decoration: formStyle('Username or Gmail', Icons.person),
                  controller: nameController,
                  // The validator receives the text that the user has entered.
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Please enter username';
                    }
                    return null;
                  },
                  onFieldSubmitted: (_) async => await _handleSubmit(),
                ),
                SizedBox(
                  height: size.height * 0.02,
                ),
                TextFormField(
                  enabled: !_isLoading,
                  style: const TextStyle(
                    color: Colors.lightBlue,
                  ),
                  cursorColor: Colors.deepPurpleAccent,
                  controller: passwordController,
                  obscureText: !_showPassword,
                  decoration: formStyle('Password', Icons.lock_open,
                      suffix: IconButton(
                        icon: Icon(
                          _showPassword
                              ? Icons.visibility
                              : Icons.visibility_off,
                          color: Colors.deepPurpleAccent,
                        ),
                        onPressed: _togglePasswordVisible,
                      )),
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Please enter some text';
                    }
                    return null;
                  },
                  onFieldSubmitted: (_) async => await _handleSubmit(),
                ),
                SizedBox(
                  height: size.height * 0.01,
                ),
                SizedBox(
                  height: size.height * 0.02,
                ),

                /// Login Button
                loginButton(_isLoading),
                SizedBox(
                  height: size.height * 0.03,
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    var size = MediaQuery.of(context).size;
    return GestureDetector(
      onTap: () => FocusManager.instance.primaryFocus?.unfocus(),
      child: Scaffold(
          backgroundColor: Colors.transparent,
          resizeToAvoidBottomInset: false,
          body: Container(
            decoration: const BoxDecoration(
                gradient: LinearGradient(
              begin: Alignment.topLeft,
              end: Alignment.bottomRight,
              colors: [
                Color.fromRGBO(4, 45, 107, 1),
                Color.fromRGBO(4, 13, 33, 1),
                Color.fromRGBO(71, 31, 73, 1),
              ],
            )),
            child: Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                if (size.width > 800)
                  Expanded(
                    flex: 4,
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        SizedBox(
                          height: size.height * 0.8,
                          child: Lottie.asset(
                            'assets/img/99797-data-management.json',
                            width: double.infinity,
                          ),
                        ),
                        SizedBox(
                          height: size.height * 0.2,
                          child: Text(
                            'Assignment Centre',
                            style: GoogleFonts.merriweather(
                              fontStyle: FontStyle.italic,
                              fontSize: 50,
                            ),
                          ),
                        )
                      ],
                    ),
                  ),
                Expanded(
                  flex: 5,
                  child: Padding(
                    padding: EdgeInsets.symmetric(
                        horizontal: size.width * 0.5 * 0.1),
                    child: _buildMainBody(size),
                  ),
                ),
              ],
            ),
          ),
          floatingActionButton: JustTheTooltip(
            preferredDirection: AxisDirection.left,
            offset: 5,
            tailLength: 10,
            tailBaseWidth: 10,
            backgroundColor: const Color.fromRGBO(66, 64, 66, 1),
            content: const Padding(
              padding: EdgeInsets.all(8.0),
              child: Text('Exit to desktop'),
            ),
            child: FloatingActionButton(
              onPressed: () async {
                await WindowManager.instance.close();
              },
              backgroundColor: const Color.fromRGBO(9, 46, 117, 1),
              child: const Icon(Icons.close),
            ),
          )),
    );
  }

  // Login Button
  Widget loginButton(bool isloading) {
    return SizedBox(
      width: double.infinity,
      height: 55,
      child: ElevatedButton(
        style: ButtonStyle(
          backgroundColor: MaterialStateProperty.all(Colors.deepPurpleAccent),
          shape: MaterialStateProperty.all(
            RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(15),
            ),
          ),
        ),
        onPressed: _handleSubmit,
        child: isloading
            ? const CircularProgressIndicator()
            : const Text(
                'Login',
                style: TextStyle(fontSize: 20),
              ),
      ),
    );
  }

  // Login Button
  InputDecoration formStyle(String hint, IconData prefixIcon,
      {Widget? suffix}) {
    return InputDecoration(
      fillColor: Colors.white,
      prefixIcon: Icon(
        prefixIcon,
        color: Colors.deepPurpleAccent,
      ),
      disabledBorder: const OutlineInputBorder(
        borderSide: BorderSide(color: Colors.blueGrey, width: 1),
        borderRadius: BorderRadius.all(Radius.circular(15)),
      ),
      hintStyle: const TextStyle(color: Colors.deepPurpleAccent),
      suffixIcon: suffix,
      hintText: hint,
      focusedBorder: const OutlineInputBorder(
        borderSide: BorderSide(color: Colors.deepPurple, width: 3.0),
        borderRadius: BorderRadius.all(Radius.circular(15)),
      ),
      enabledBorder: const OutlineInputBorder(
        borderSide: BorderSide(color: Colors.deepPurpleAccent, width: 2.0),
        borderRadius: BorderRadius.all(Radius.circular(15)),
      ),
      border: const OutlineInputBorder(
        borderRadius: BorderRadius.all(Radius.circular(15)),
      ),
    );
  }
}
