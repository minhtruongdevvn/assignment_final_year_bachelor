import 'package:agent_assignment_app/widgets/agent/table.dart';
import 'package:agent_assignment_app/widgets/common_screen.dart';
import 'package:flutter/material.dart';

class AgentScreen extends StatefulWidget {
  static const String route = '/agents';
  const AgentScreen({super.key});

  @override
  State<AgentScreen> createState() => _AgentScreenState();
}

class _AgentScreenState extends State<AgentScreen> {
  @override
  Widget build(BuildContext context) {
    return _buildMainWrapper(child: const AgentTable());
  }

  Widget _buildMainWrapper({required Widget child}) {
    return CommonScreen(
        child: Container(
            margin: const EdgeInsets.all(20),
            width: double.infinity,
            height: double.infinity,
            decoration: BoxDecoration(
                color: const Color.fromRGBO(39, 40, 49, 1),
                borderRadius: BorderRadius.circular(10)),
            child: child));
  }
}
