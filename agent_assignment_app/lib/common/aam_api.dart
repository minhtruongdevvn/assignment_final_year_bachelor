import 'dart:convert';
import 'dart:io';

import 'package:agent_assignment_app/env.dart';
import 'package:agent_assignment_app/models/pagination.dart';
import 'package:agent_assignment_app/models/response_wrapper.dart';
import 'package:agent_assignment_app/services/providers/auth.dart';
import 'package:flutter/cupertino.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';

abstract class AAMAPI {
  @protected
  static final String aamUrl = EnvironmentConfig.envVars.aamUrl;

  @protected
  static String? get accessToken {
    return Auth.token;
  }

  @protected
  static Future<PageResult<TResult>> getWithPaging<TResult>(String endpoint,
      String query, TResult Function(Map<String, dynamic>) deserializer) async {
    final response =
        await http.get(Uri.parse('${AAMAPI.aamUrl}/$endpoint$query'), headers: {
      HttpHeaders.authorizationHeader: 'Bearer ${AAMAPI.accessToken}',
    });

    if (response.statusCode != 200) {
      // handler
    }

    final responseData = Map<String, dynamic>.from(json.decode(response.body));
    return PageResult<TResult>.fromJson(responseData, deserializer);
  }

  @protected
  static Future<ResponseWrapper> getUrl<TResult>(String endpoint) async {
    final response =
        await http.get(Uri.parse('${AAMAPI.aamUrl}/$endpoint'), headers: {
      HttpHeaders.authorizationHeader: 'Bearer ${AAMAPI.accessToken}',
    });

    if (response.statusCode != 200) {
      return ResponseWrapper(response, false);
    }

    return ResponseWrapper(response);
  }

  @protected
  static Future<Response> postUrl(String endpoint,
      [Map<String, dynamic>? body]) async {
    if (body != null) {
      body.forEach((key, value) {
        if (value is DateTime) {
          body.update(key, (e) => value.toIso8601String());
        }
      });
    }
    final response = await http.post(Uri.parse('${AAMAPI.aamUrl}/$endpoint'),
        headers: {
          HttpHeaders.contentTypeHeader: 'application/json',
          HttpHeaders.authorizationHeader: 'Bearer ${AAMAPI.accessToken}',
        },
        body: body == null ? null : json.encode(body));

    if (response.statusCode != 200) {
      // handler
    }

    return response;
  }

  @protected
  static Future<void> putUrl<TResult>(
      String endpoint, Map<String, dynamic> body) async {
    body.forEach((key, value) {
      if (value is DateTime) {
        body.update(key, (e) => value.toIso8601String());
      }
    });
    final response = await http.put(Uri.parse('${AAMAPI.aamUrl}/$endpoint'),
        headers: {
          HttpHeaders.contentTypeHeader: 'application/json',
          HttpHeaders.authorizationHeader: 'Bearer ${AAMAPI.accessToken}',
        },
        body: json.encode(body));

    if (response.statusCode != 200) {
      // handler
    }
  }

  @protected
  static Future<void> deleteUrl(String endpoint) async {
    final response =
        await http.delete(Uri.parse('${AAMAPI.aamUrl}/$endpoint'), headers: {
      HttpHeaders.authorizationHeader: 'Bearer ${AAMAPI.accessToken}',
    });

    if (response.statusCode != 200) {
      // handler
    }
  }
}
