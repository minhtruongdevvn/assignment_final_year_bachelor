import 'package:agent_assignment_app/helpers/String.dart';
import 'package:agent_assignment_app/models/agent.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';

class AgentDetail extends StatelessWidget {
  final Agent agent;
  final scrollController = ScrollController();
  AgentDetail(
    this.agent, {
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Scrollbar(
      thumbVisibility: true,
      controller: scrollController,
      child: GridView.count(
        controller: scrollController,
        childAspectRatio: 2.7,
        primary: false,
        padding: const EdgeInsets.all(5),
        crossAxisSpacing: 50,
        mainAxisSpacing: 5,
        crossAxisCount: 3,
        children: [
          _buildItem('Family Name', agent.familyName),
          _buildItem('Given Name', agent.givenName),
          _buildItem('Code', agent.code),
          _buildItem('Sex', agent.sex ? 'Female' : 'Male'),
          _buildItem(
              'Birth Date', DateFormat('dd/MM/yyy').format(agent.birthDate)),
          _buildItem('Self Discipline ', agent.selfDiscipline.toString()),
          _buildItem('Height', '${agent.height} cm'),
          _buildItem('IQ ', agent.iq.toString()),
          _buildItem('EQ', agent.eq.toString()),
          _buildItem('Stamina', agent.stamina.toString()),
          _buildItem('Strength', '${agent.strength} kg'),
          _buildItem('Reaction Time', '${agent.reactionTime} ms'),
          if (agent.agentSkills.isNotEmpty)
            ...agent.agentSkills
                .map((el) => _buildItem(displayFromSnakeStyle(el.name),
                    el.score.toStringAsFixed(1)))
                .toList()
        ],
      ),
    );
  }

  Widget _buildItem(String title, String? value) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: GoogleFonts.arvo(fontSize: 18, color: Colors.grey.shade400),
        ),
        const SizedBox(
          height: 5,
        ),
        Text(
          value ?? 'Empty',
          style: GoogleFonts.roboto(
              fontSize: 16,
              fontStyle: value == null ? FontStyle.italic : FontStyle.normal),
        )
      ],
    );
  }
}
