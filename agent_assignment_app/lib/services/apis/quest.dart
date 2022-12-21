import 'dart:convert';

import 'package:agent_assignment_app/common/aam_api.dart';
import 'package:agent_assignment_app/common/constant.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:agent_assignment_app/models/pagination.dart';
import 'package:agent_assignment_app/models/predict_result.dart';
import 'package:agent_assignment_app/models/quest.dart';
import 'package:http/http.dart';

class QuestAPI extends AAMAPI {
  static Future<PageResult<Quest>> getWithPaging(String query) {
    return AAMAPI.getWithPaging<Quest>('quests', query, Quest.fromJson);
  }

  static Future<String> addQuest(Map<String, Object?> quest) async {
    final response = await AAMAPI.postUrl('quests', quest);
    final responseData = json.decode(response.body) as String;
    return responseData;
  }

  static Future<void> editQuest(String id, Map<String, Object?> quest) {
    return AAMAPI.putUrl('quests/$id', quest);
  }

  static Future<void> deleteQuest(String questId) {
    return AAMAPI.deleteUrl('quests/$questId');
  }

  static Future<Quest> getQuestById(String id) async {
    final responseWrapper = await AAMAPI.getUrl('quests/$id');
    final responseData = responseWrapper.getBody();
    return Quest.fromJson(responseData);
  }

  static Future<Response> addAgentToQuest(
      String questId, String agentId, PredictResult predictResult) {
    return AAMAPI.postUrl(
        'quests/$questId/agents/$agentId', predictResult.toJson());
  }

  static Future<Response> addAgentToQuestWithCode(
      String questCode, String agentId, PredictResult predictResult) {
    return AAMAPI.postUrl(
        'quests/code/$questCode/agents/$agentId', predictResult.toJson());
  }

  static Future<void> removeAgentFromQuest(String questId, String agentId) {
    return AAMAPI.deleteUrl('quests/$questId/agents/$agentId');
  }

  static Future<List<Agent>> getAgentFromQuest(String questId) async {
    var responseWrapper = await AAMAPI.getUrl('quests/$questId/agents');
    final responseData = responseWrapper.getBody();
    return List<Map<String, dynamic>>.from(responseData).map((e) {
      final agent = Agent.fromJson(e['agent']);
      agent.predictResult = PredictResult.fromJson(e['predict']);
      return agent;
    }).toList();
  }

  static Future<List<Agent>> getQuestSuggestedAgents(String questId) async {
    final responseWrapper =
        await AAMAPI.getUrl('quests/$questId/agents/suggested');
    final responseData = responseWrapper.getBody();
    return List<Map<String, dynamic>>.from(responseData).map((e) {
      final agent = Agent.fromJson(e['agent']);
      agent.predictResult = PredictResult.fromJson(e['predict']);
      return agent;
    }).toList();
  }

  static Future<PredictResult> getQuestSuggestedAgentWithCode(
      String questCode, String agentId) async {
    final responseWrapper =
        await AAMAPI.getUrl('quests/code/$questCode/agents/$agentId/suggested');

    if (!responseWrapper.isSuccess) {
      final failedResponse = responseWrapper.getErrorResponse();
      final result = PredictResult(false, 0, 0);
      switch (failedResponse.errors.first.code) {
        case ErrorType.lockedQuest:
          result.metaData.putIfAbsent('error', () => 'isQuestLock');
          break;
        case ErrorType.assigned:
          result.metaData.putIfAbsent('error', () => 'assigned');
          break;
        default:
          break;
      }

      return result;
    }

    final responseData = responseWrapper.getBody();
    return PredictResult.fromJson(responseData);
  }
}
