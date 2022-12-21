import 'package:agent_app/models/quest_category.dart';
import 'package:flutter/material.dart';

// important: dont change order
enum QuestStatus { created, waiting, onGoing, onHold, success, failed }

enum QuestNecessity {
  asap,
  needTime,
  urgent,
}

extension QuestNecessityParsing on QuestNecessity {
  String getDisplayStatus() {
    switch (this) {
      case QuestNecessity.asap:
        return 'ASAP';
      case QuestNecessity.needTime:
        return 'Amble';
      case QuestNecessity.urgent:
        return 'Urgent';
    }
  }
}

extension QuestStatusParsing on QuestStatus {
  String getDisplayStatus() {
    switch (this) {
      case QuestStatus.created:
        return 'Created';
      case QuestStatus.waiting:
        return 'Waiting to process';
      case QuestStatus.onGoing:
        return 'On going';
      case QuestStatus.onHold:
        return 'On hold';
      case QuestStatus.success:
        return 'Succeeded';
      case QuestStatus.failed:
        return 'Failed';
    }
  }

  Color getColorStatus() {
    switch (this) {
      case QuestStatus.created:
        return Colors.lightBlue;
      case QuestStatus.waiting:
        return Colors.grey;
      case QuestStatus.onGoing:
        return Colors.yellow.shade600;
      case QuestStatus.onHold:
        return Colors.orangeAccent;
      case QuestStatus.success:
        return Colors.greenAccent;
      case QuestStatus.failed:
        return Colors.redAccent;
    }
  }

  IconData getIconStatus() {
    switch (this) {
      case QuestStatus.created:
        return Icons.create;
      case QuestStatus.waiting:
        return Icons.pending;
      case QuestStatus.onGoing:
        return Icons.run_circle_outlined;
      case QuestStatus.onHold:
        return Icons.lock_clock;
      case QuestStatus.success:
        return Icons.done_all;
      case QuestStatus.failed:
        return Icons.error;
    }
  }
}

class Quest {
  final String id;
  final DateTime dateCreated;
  final DateTime dateModified;
  final String? context;
  int questStatus;
  final int necessity;
  final DateTime? expired;
  final String categoryId;
  final String code;
  final int numberOfAgent;
  final QuestCategory category;
  Quest(
    this.id,
    this.dateCreated,
    this.dateModified,
    this.context,
    this.questStatus,
    this.necessity,
    this.expired,
    this.categoryId,
    this.category,
    this.code,
    this.numberOfAgent,
  );

  factory Quest.fromJson(Map<String, dynamic> json) => Quest(
        json['id'] as String,
        DateTime.parse(json['dateCreated'] as String),
        DateTime.parse(json['dateModified'] as String),
        json['context'] as String?,
        json['questStatus'] as int,
        json['necessity'] as int,
        json['expired'] == null
            ? null
            : DateTime.parse(json['expired'] as String),
        json['categoryId'] as String,
        QuestCategory.fromJson(json['category'] as Map<String, dynamic>),
        json['code'] as String,
        json['numberOfAgent'] as int,
      );

  factory Quest.defaultValue() => Quest(
        '',
        DateTime(2000),
        DateTime(2000),
        '',
        0,
        0,
        null,
        '',
        QuestCategory('', DateTime(2000), DateTime(2000), '', ''),
        '',
        0,
      );
}
