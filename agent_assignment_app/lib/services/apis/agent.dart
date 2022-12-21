import 'package:agent_assignment_app/common/aam_api.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/pagination.dart';

class AgentAPI extends AAMAPI {
  static Future<PageResult<Agent>> getWithPaging(String query) {
    return AAMAPI.getWithPaging<Agent>('agents', query, Agent.fromJson);
  }

  static Future<Agent> getById(String id) async {
    final responseWrapper = await AAMAPI.getUrl('agents/$id');
    final responseData = responseWrapper.getBody();
    return Agent.fromJson(responseData);
  }
}
