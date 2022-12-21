import 'package:agent_assignment_app/common/aam_api.dart';
import 'package:agent_assignment_app/models/quest_category.dart';

class QuestCatgoryAPI extends AAMAPI {
  static Future<List<QuestCategory>> getAll() async {
    final responseWrapper = await AAMAPI.getUrl('quests/categories');
    final responseData =
        List<Map<String, dynamic>>.from(responseWrapper.getBody());
    return responseData
        .map(
          (e) => QuestCategory.fromJson(e),
        )
        .toList();
  }
}
