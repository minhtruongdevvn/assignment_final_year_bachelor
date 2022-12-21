import 'dart:core';

import 'package:agent_assignment_app/models/quest_category.dart';
import 'package:agent_assignment_app/services/apis/quest_category.dart';
import 'package:flutter/foundation.dart';

class CateogryProvider with ChangeNotifier {
  List<QuestCategory> _categories = List<QuestCategory>.empty();

  List<QuestCategory>? get categories {
    if (_categories.isEmpty) {
      QuestCatgoryAPI.getAll().then(
        (value) {
          _categories = value;
          notifyListeners();
        },
      );
    }
    return _categories;
  }
}
