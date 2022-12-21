import 'package:agent_assignment_app/common/based_model.dart';
// import 'package:json_annotation/json_annotation.dart';

// part 'quest_category.g.dart';
// //command: dart run build_runner build

// @JsonSerializable()
class QuestCategory extends BaseModel {
  final String name;
  final String description;
  QuestCategory(
    super.id,
    super.dateCreated,
    super.dateModified,
    this.name,
    this.description,
  );

  @override
  List<Object?> get props => super.props..addAll([name, description]);

  factory QuestCategory.fromJson(Map<String, dynamic> json) => QuestCategory(
        json['id'] as String,
        DateTime.parse(json['dateCreated'] as String),
        DateTime.parse(json['dateModified'] as String),
        json['name'] as String,
        json['description'] as String,
      );

  Map<String, dynamic> toJson() => <String, dynamic>{
        'id': id,
        'dateCreated': dateCreated.toIso8601String(),
        'dateModified': dateModified.toIso8601String(),
        'name': name,
        'description': description,
      };
}
