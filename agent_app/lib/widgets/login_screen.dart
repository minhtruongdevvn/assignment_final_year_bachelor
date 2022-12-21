import 'dart:io';

import 'package:agent_app/providers/auth.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class LoginScreen extends StatefulWidget {
  static String tag = 'login-screen';

  const LoginScreen({super.key});
  @override
  LoginScreenState createState() => LoginScreenState();
}

class LoginScreenState extends State<LoginScreen> {
  bool _validateEmail = true;
  bool _validatePassword = true;
  bool _isLoading = false;
  final passwordController = TextEditingController();
  final emainController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    final logo = Hero(
      tag: 'hero',
      child: CircleAvatar(
        backgroundColor: Colors.transparent,
        radius: 48.0,
        child: Image.asset('assets/logo.png'),
      ),
    );

    final email = TextFormField(
      controller: emainController,
      keyboardType: TextInputType.emailAddress,
      onChanged: (value) {
        if (value.isEmpty && _validateEmail) {
          setState(() {
            _validateEmail = false;
          });
        } else if (value.isNotEmpty && !_validateEmail) {
          setState(() {
            _validateEmail = true;
          });
        }
      },
      autofocus: false,
      decoration: InputDecoration(
        hintStyle: const TextStyle(color: Colors.white),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(32.0),
          borderSide: const BorderSide(color: Colors.white),
        ),
        hintText: 'Email',
        errorText: _validateEmail ? null : 'Email Can\'t Be Empty',
        contentPadding: const EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
      ),
    );

    final password = TextFormField(
      controller: passwordController,
      onChanged: (value) {
        if (value.isEmpty && _validatePassword) {
          setState(() {
            _validatePassword = false;
          });
        } else if (value.isNotEmpty && !_validatePassword) {
          setState(() {
            _validatePassword = true;
          });
        }
      },
      autofocus: false,
      obscureText: true,
      decoration: InputDecoration(
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(32.0),
          borderSide: const BorderSide(color: Colors.white),
        ),
        hintStyle: const TextStyle(color: Colors.white),
        hintText: 'Password',
        errorText: _validatePassword ? null : 'Password Can\'t Be Empty',
        contentPadding: const EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
      ),
    );

    final loginButton = Padding(
      padding: const EdgeInsets.symmetric(vertical: 16.0),
      child: ElevatedButton(
        style: ButtonStyle(
            padding: MaterialStateProperty.all(
                const EdgeInsets.symmetric(vertical: 20)),
            backgroundColor: _validateEmail && _validatePassword
                ? MaterialStateProperty.all(Colors.lightBlueAccent)
                : MaterialStateProperty.all(
                    Colors.lightBlueAccent.withOpacity(0.5)),
            shape: MaterialStateProperty.all(RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(24),
            ))),
        onPressed: _validateEmail && _validatePassword
            ? () async {
                if (passwordController.text.isEmpty ||
                    emainController.text.isEmpty) {
                  setState(() {
                    if (passwordController.text.isEmpty) {
                      _validatePassword = false;
                    }
                    if (emainController.text.isEmpty) {
                      _validatePassword = false;
                    }
                  });
                  return;
                }
                setState(() {
                  _isLoading = true;
                });
                try {
                  final isSuccess =
                      await Provider.of<Auth>(context, listen: false).login(
                    emainController.text,
                    passwordController.text,
                  );
                  if (!isSuccess) {
                    _showErrorDialog('Permission denied');
                  }
                } on HttpException catch (error) {
                  var errorMessage = 'Authentication failed';
                  if (error
                      .toString()
                      .contains('invalid_username_or_password')) {
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
            : null,
        child: _isLoading
            ? const CircularProgressIndicator()
            : const Text('Log In', style: TextStyle(color: Colors.white)),
      ),
    );

    final forgotLabel = TextButton(
      child: const Text(
        'Forgot password?',
      ),
      onPressed: () {},
    );

    return Scaffold(
      backgroundColor: const Color.fromRGBO(31, 60, 74, 1),
      body: Center(
        child: ListView(
          shrinkWrap: true,
          padding: const EdgeInsets.only(left: 24.0, right: 24.0),
          children: <Widget>[
            logo,
            const SizedBox(height: 48.0),
            email,
            const SizedBox(height: 8.0),
            password,
            const SizedBox(height: 24.0),
            loginButton,
            forgotLabel
          ],
        ),
      ),
    );
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
}
