import 'package:agent_assignment_app/common/based_model.dart';

class Transaction extends BaseModel {
  final int questStatus;
  Transaction(
      super.id, super.dateCreated, super.dateModified, this.questStatus);

  factory Transaction.fromJson(Map<String, dynamic> json) => Transaction(
        json['id'] as String,
        DateTime.parse(json['dateCreated'] as String),
        DateTime.parse(json['dateModified'] as String),
        json['questStatus'] as int,
      );
}
