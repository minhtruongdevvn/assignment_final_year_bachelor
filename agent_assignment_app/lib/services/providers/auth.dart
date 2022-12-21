import 'dart:async';
import 'dart:convert';
import 'dart:io';

import 'package:agent_assignment_app/env.dart';
import 'package:flutter/widgets.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

class Auth with ChangeNotifier {
  final String idsUrl = EnvironmentConfig.envVars.idsUrl;
  static String? _token;
  String? _refreshToken;
  static DateTime? _expiryDate;
  String? _userId;
  String? familyName;
  String? code;
  String? givenName;
  Timer? _authTimer;

  bool get isAuth {
    return token != null;
  }

  String? get userId {
    return _userId;
  }

  static String? get token {
    if (_expiryDate == null || _expiryDate!.isBefore(DateTime.now())) {
      _token = null;
    }
    return _token;
  }

  Future<bool> login(String emailOrUsername, String password) async {
    final url = Uri.parse('${EnvironmentConfig.envVars.idsUrl}/connect/token');
    try {
      var form = <String, dynamic>{};
      form['client_id'] = EnvironmentConfig.IDS_CLIENT_ID;
      form['client_secret'] = EnvironmentConfig.IDS_CLIENT_SECRET;
      form['grant_type'] = 'password';
      form['username'] = emailOrUsername;
      form['password'] = password;

      final response = await http.post(
        url,
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
        body: form,
      );
      final responseData = json.decode(response.body);
      if (responseData['error'] != null) {
        throw HttpException(responseData['error_description']);
      }
      if (responseData['belong_to'] == null ||
          responseData['belong_to'] != 'AAM') {
        return false;
      }

      _token = responseData['access_token'];
      _refreshToken = responseData['refresh_token'];
      _userId = responseData['user_id'];
      familyName = responseData['family_name'] as String?;
      givenName = responseData['given_name'] as String?;
      code = responseData['internal_code'] as String?;
      _expiryDate = DateTime.now().add(
        Duration(
          seconds: responseData['expires_in'] - 5,
        ),
      );

      _startSilentOperations();
      notifyListeners();
      final prefs = await SharedPreferences.getInstance();
      final userData = json.encode(
        {
          'access_token': _token,
          'refresh_token': _refreshToken,
          'user_id': _userId,
          'family_name': familyName,
          'given_name': givenName,
          'internal_code': code,
          'expires_in': _expiryDate!.toIso8601String(),
        },
      );
      prefs.setString('userData', userData);
      return true;
    } catch (error) {
      rethrow;
    }
  }

  Future<void> tryAutoLogin() async {
    final prefs = await SharedPreferences.getInstance();
    if (!prefs.containsKey('userData')) {
      return;
    }
    final extractedUserData =
        json.decode(prefs.getString('userData')!) as Map<String, dynamic>;
    final expiryDate =
        DateTime.parse(extractedUserData['expires_in'] as String);

    if (expiryDate.isBefore(DateTime.now())) {
      await _silentRenew();
      return;
    }
    _token = extractedUserData['access_token'] as String?;
    _refreshToken = extractedUserData['refresh_token'] as String?;
    _userId = extractedUserData['user_id'] as String?;
    familyName = extractedUserData['family_name'] as String?;
    givenName = extractedUserData['given_name'] as String?;
    code = extractedUserData['internal_code'] as String?;
    _expiryDate = expiryDate;
    notifyListeners();
    _startSilentOperations();
    return;
  }

  Future<void> logout() async {
    _token = null;
    _refreshToken = null;
    _userId = null;
    familyName = null;
    givenName = null;
    code = null;
    _expiryDate = null;
    if (_authTimer != null) {
      _authTimer!.cancel();
      _authTimer = null;
    }
    final prefs = await SharedPreferences.getInstance();
    // prefs.remove('userData');
    prefs.clear();
    notifyListeners();
  }

  Future<void> _silentRenew() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final url =
          Uri.parse('${EnvironmentConfig.envVars.idsUrl}/connect/token');

      var form = <String, dynamic>{};
      form['client_id'] = EnvironmentConfig.IDS_CLIENT_ID;
      form['client_secret'] = EnvironmentConfig.IDS_CLIENT_SECRET;
      form['grant_type'] = 'refresh_token';

      if (_refreshToken == null) {
        final extractedUserData =
            json.decode(prefs.getString('userData')!) as Map<String, dynamic>;
        _refreshToken = extractedUserData['refresh_token'];
      } else {
        form['refresh_token'] = _refreshToken;
      }

      final response = await http.post(
        url,
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
        body: form,
      );
      if (response.statusCode != 200) {
        logout();
        return;
      }

      final responseData = json.decode(response.body);
      _token = responseData['access_token'];
      _refreshToken = responseData['refresh_token'];
      _userId = responseData['user_id'];
      _expiryDate = DateTime.now().add(
        Duration(
          seconds: responseData['expires_in'] - 5,
        ),
      );
      _startSilentOperations();
      notifyListeners();
      final userData = json.encode(
        {
          'access_token': _token,
          'refresh_token': _refreshToken,
          'user_id': _userId,
          'family_name': familyName,
          'given_name': givenName,
          'internal_code': code,
          'expires_in': _expiryDate!.toIso8601String(),
        },
      );
      prefs.setString('userData', userData);
    } catch (error) {
      rethrow;
    }
  }

  void _startSilentOperations() {
    if (_authTimer != null) {
      _authTimer!.cancel();
    }
    final timeToExpiry = _expiryDate!.difference(DateTime.now()).inSeconds;
    _authTimer = Timer(Duration(seconds: timeToExpiry), (() async {
      await _silentRenew();
    }));
  }
}
