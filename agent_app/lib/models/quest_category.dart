class QuestCategory {
  final String id;
  final DateTime dateCreated;
  final DateTime dateModified;
  final String name;
  final String description;
  QuestCategory(
    this.id,
    this.dateCreated,
    this.dateModified,
    this.name,
    this.description,
  );

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
