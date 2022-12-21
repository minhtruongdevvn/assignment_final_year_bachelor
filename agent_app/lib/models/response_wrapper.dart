import 'dart:convert';

import 'package:agent_app/constant.dart';
import 'package:http/http.dart';

class ResponseWrapper {
  final Response _response;
  final bool isSuccess;

  ResponseWrapper(this._response, [this.isSuccess = true]);

  dynamic getBody() {
    if (!isSuccess) throw Exception('Cannot get body from a failed response');
    return json.decode(_response.body);
  }

  ErrorResponse getErrorResponse() {
    if (isSuccess) {
      throw Exception('Cannot a get failed response from a non-error body');
    }
    return ErrorResponse.fromJson(json.decode(_response.body));
  }
}

class ErrorResponse {
  final List<ErrorResponseItem> errors;
  final String? metadata;

  ErrorResponse({required this.errors, this.metadata});

  factory ErrorResponse.fromJson(Map<String, dynamic> json) => ErrorResponse(
        errors: List<Map<String, dynamic>>.from(json["Errors"])
            .map((e) =>
                ErrorResponseItem(e['Error'], ErrorType.values[e['Code']]))
            .toList(),
      );
}

class ErrorResponseItem {
  final String error;
  final ErrorType code;

  ErrorResponseItem(this.error, this.code);
}
