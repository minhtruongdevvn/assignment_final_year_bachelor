import 'package:agent_assignment_app/models/predict_result.dart';
import 'package:equatable/equatable.dart';

// ignore: must_be_immutable
class Agent extends Equatable {
  final String id;
  final String email;
  final String familyName;
  final String givenName;
  final DateTime birthDate;
  final String? picture;
  final bool sex;
  final String code;
  final String identityReference;
  final int selfDiscipline;
  final double height;
  final double iq;
  final double eq;
  final double stamina;
  final double strength;
  final double reactionTime;
  bool? isBusy;
  PredictResult? predictResult;
  final List<AgentSkill> agentSkills;
  bool? assignedToTheQuest;

  Agent(
      this.id,
      this.email,
      this.familyName,
      this.givenName,
      this.birthDate,
      this.picture,
      this.sex,
      this.code,
      this.identityReference,
      this.selfDiscipline,
      this.height,
      this.iq,
      this.eq,
      this.stamina,
      this.strength,
      this.reactionTime,
      this.agentSkills,
      {this.isBusy});

  factory Agent.fromJson(Map<String, dynamic> json) => Agent(
        isBusy: json['isBusy'] as bool?,
        json['id'] as String,
        json['email'] as String,
        json['familyName'] as String,
        json['givenName'] as String,
        DateTime.parse(json['birthDate'] as String),
        json['picture'] as String?,
        json['sex'] as bool,
        json['code'] as String,
        json['identityReference'] as String,
        json['selfDiscipline'] as int,
        (json['height'] as num).toDouble(),
        (json['iq'] as num).toDouble(),
        (json['eq'] as num).toDouble(),
        (json['stamina'] as num).toDouble(),
        (json['strength'] as num).toDouble(),
        (json['reactionTime'] as num).toDouble(),
        json["agentSkills"] == null
            ? []
            : List<Map<String, dynamic>>.from(json["agentSkills"])
                .map((e) => AgentSkill.fromJson(e))
                .toList(),
      );

  @override
  List<Object?> get props => [
        email,
        familyName,
        givenName,
        birthDate,
        picture,
        sex,
        code,
        identityReference,
        selfDiscipline,
        height,
        iq,
        eq,
        stamina,
        strength,
        reactionTime,
        assignedToTheQuest
      ];
}

class AgentSkill {
  final double score;
  final String name;

  AgentSkill(this.score, this.name);

  factory AgentSkill.fromJson(Map<String, dynamic> json) => AgentSkill(
      (json['score'] as num).toDouble(), json['skillName'] as String);
}
