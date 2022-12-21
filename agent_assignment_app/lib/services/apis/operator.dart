import 'package:agent_assignment_app/common/aam_api.dart';
import 'package:http/http.dart';

class OperatorAPI extends AAMAPI {
  static Future<Response> addOperator(Map<String, Object?> addedOperator) {
    return AAMAPI.postUrl('operators', addedOperator);
  }
}
