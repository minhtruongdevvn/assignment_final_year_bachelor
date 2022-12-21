import 'dart:convert';
import 'dart:io';

import 'package:agent_app/env.dart';
import 'package:agent_app/models/response_wrapper.dart';
import 'package:agent_app/providers/auth.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class API {
  static final String _aamUrl = EnvironmentConfig.envVars.aamUrl;

  static String? get _accessToken {
    return Auth.token;
  }

  static Future<ResponseWrapper> getUrl<TResult>(String endpoint) async {
    final response = await http.get(Uri.parse('$_aamUrl/$endpoint'), headers: {
      HttpHeaders.authorizationHeader: 'Bearer $_accessToken',
    });

    if (response.statusCode != 200) {
      return ResponseWrapper(response, false);
    }

    return ResponseWrapper(response);
  }

  static Future<Response> postUrl(String endpoint,
      [Map<String, dynamic>? body]) async {
    if (body != null) {
      body.forEach((key, value) {
        if (value is DateTime) {
          body.update(key, (e) => value.toIso8601String());
        }
      });
    }
    final response = await http.post(Uri.parse('$_aamUrl/$endpoint'),
        headers: {
          HttpHeaders.contentTypeHeader: 'application/json',
          HttpHeaders.authorizationHeader: 'Bearer $_accessToken',
        },
        body: body == null ? null : json.encode(body));

    if (response.statusCode != 200) {
      // handler
    }

    return response;
  }

  static Future<void> putUrl<TResult>(
      String endpoint, Map<String, dynamic> body) async {
    body.forEach((key, value) {
      if (value is DateTime) {
        body.update(key, (e) => value.toIso8601String());
      }
    });
    final response = await http.put(Uri.parse('$_aamUrl/$endpoint'),
        headers: {
          HttpHeaders.contentTypeHeader: 'application/json',
          HttpHeaders.authorizationHeader: 'Bearer $_accessToken',
        },
        body: json.encode(body));

    if (response.statusCode != 200) {
      // handler
    }
  }

  static Future<void> patchUrl<TResult>(String endpoint,
      [Map<String, dynamic>? body]) async {
    if (body != null) {
      body.forEach((key, value) {
        if (value is DateTime) {
          body.update(key, (e) => value.toIso8601String());
        }
      });
    }

    final response = await http.patch(Uri.parse('$_aamUrl/$endpoint'),
        headers: {
          HttpHeaders.contentTypeHeader: 'application/json',
          HttpHeaders.authorizationHeader: 'Bearer $_accessToken',
        },
        body: body == null ? null : json.encode(body));

    if (response.statusCode != 200) {
      // handler
    }
  }

  static Future<void> deleteUrl(String endpoint) async {
    final response =
        await http.delete(Uri.parse('$_aamUrl/$endpoint'), headers: {
      HttpHeaders.authorizationHeader: 'Bearer $_accessToken',
    });

    if (response.statusCode != 200) {
      // handler
    }
  }
}
