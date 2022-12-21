// important: dont change order
enum ErrorType {
  entityNotFound,
  lockedQuest,
  invalidOperation,
  cannotExecuteAction,
  assigned
}

class HubAction {
  static const String add = "AgentQuestAdd";
  static const String delete = "AgentQuestDelete";
  static const String update = "AgentQuestUpdate";
}
