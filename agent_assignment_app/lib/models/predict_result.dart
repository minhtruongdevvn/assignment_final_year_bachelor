import 'package:equatable/equatable.dart';

class PredictResult extends Equatable {
  final bool success;
  final double score;
  final double probability;
  Map<String, dynamic> metaData = {};

  PredictResult(
    this.success,
    this.score,
    this.probability,
  );

  factory PredictResult.fromJson(Map<String, dynamic> json) => PredictResult(
        json['success'] as bool,
        (json['score'] as num).toDouble(),
        (json['probability'] as num).toDouble(),
      );

  Map<String, dynamic> toJson() => <String, Object?>{
        'success': success,
        'score': score,
        'probability': probability,
      };

  @override
  List<Object?> get props => [
        success,
        score,
        probability,
      ];
}
