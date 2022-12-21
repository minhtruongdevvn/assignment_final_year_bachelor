import 'package:agent_assignment_app/services/providers/auth.dart';
import 'package:agent_assignment_app/widgets/agent/screen.dart';
import 'package:agent_assignment_app/widgets/custom/app_icons.dart';
import 'package:agent_assignment_app/widgets/drawer/bottom_user_info.dart';
import 'package:agent_assignment_app/widgets/drawer/custom_list_tile.dart';
import 'package:agent_assignment_app/widgets/operator_create.dart';
import 'package:agent_assignment_app/widgets/quest/screen.dart';
import 'package:flutter/material.dart';
import 'package:just_the_tooltip/just_the_tooltip.dart';
import 'package:provider/provider.dart';
import 'package:window_manager/window_manager.dart';

class CustomDrawer extends StatefulWidget {
  const CustomDrawer({Key? key}) : super(key: key);

  @override
  State<CustomDrawer> createState() => _CustomDrawerState();
}

class _CustomDrawerState extends State<CustomDrawer> {
  bool _isCollapsed = false;

  @override
  Widget build(BuildContext context) {
    final currentRoute = ModalRoute.of(context)!.settings.name;
    final authProvider = Provider.of<Auth>(context, listen: false);
    final givenName =
        authProvider.givenName == null ? '' : authProvider.givenName![0];
    return SafeArea(
      child: AnimatedContainer(
        curve: Curves.easeInOutCubic,
        duration: const Duration(milliseconds: 150),
        width: _isCollapsed ? 300 : 70,
        margin: const EdgeInsets.only(bottom: 10, top: 10),
        decoration: const BoxDecoration(
          borderRadius: BorderRadius.only(
            bottomRight: Radius.circular(10),
            topRight: Radius.circular(10),
          ),
          color: Color.fromRGBO(20, 20, 20, 1),
        ),
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 10),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              const SizedBox(
                height: 40,
              ),
              CustomListTile(
                onPressed: () {
                  Navigator.of(context).pushReplacementNamed(QuestScreen.route);
                },
                isCollapsed: _isCollapsed,
                isSelected:
                    currentRoute == QuestScreen.route || currentRoute == '/',
                icon: AppIcons.quest,
                title: 'Quests',
                infoCount: 0,
              ),
              CustomListTile(
                onPressed: () {
                  Navigator.of(context).pushReplacementNamed(AgentScreen.route);
                },
                isCollapsed: _isCollapsed,
                isSelected: currentRoute == AgentScreen.route,
                icon: Icons.people_rounded,
                title: 'Agents',
                infoCount: 0,
              ),
              CustomListTile(
                onPressed: () {
                  Navigator.of(context)
                      .pushReplacementNamed(OperatorCreate.route);
                },
                isCollapsed: _isCollapsed,
                isSelected: currentRoute == OperatorCreate.route,
                icon: AppIcons.operator_,
                title: 'Operators',
                infoCount: 0,
              ),
              const Spacer(),
              BottomUserInfo(
                isCollapsed: _isCollapsed,
                role: 'Operator',
                code: authProvider.code == null ? '' : '(${authProvider.code})',
                name: authProvider.familyName == null
                    ? ''
                    : '${authProvider.familyName}. $givenName',
              ),
              Container(
                margin: const EdgeInsets.symmetric(vertical: 20),
                child: JustTheTooltip(
                  preferredDirection: AxisDirection.right,
                  tailLength: 10,
                  tailBaseWidth: 10,
                  backgroundColor: const Color.fromRGBO(66, 64, 66, 1),
                  content: !_isCollapsed
                      ? const Padding(
                          padding: EdgeInsets.all(8.0),
                          child: Text('Exit to desktop'),
                        )
                      : const Text(''),
                  child: Container(
                    decoration: !_isCollapsed
                        ? const BoxDecoration(
                            shape: BoxShape.circle,
                            color: Colors.redAccent,
                          )
                        : const BoxDecoration(
                            color: Colors.redAccent,
                            borderRadius:
                                BorderRadius.all(Radius.circular(15))),
                    child: CustomListTile(
                      onPressed: () async {
                        await WindowManager.instance.close();
                      },
                      isSelected: false,
                      isCollapsed: _isCollapsed,
                      icon: Icons.close,
                      title: 'Exit to desktop',
                      infoCount: 0,
                    ),
                  ),
                ),
              ),
              Container(
                margin: const EdgeInsets.only(bottom: 10),
                child: Align(
                  alignment: Alignment.center,
                  child: SizedBox(
                    width: double.infinity,
                    child: IconButton(
                      padding: const EdgeInsets.symmetric(vertical: 10.0),
                      splashColor: Colors.transparent,
                      icon: Icon(
                        _isCollapsed
                            ? Icons.keyboard_double_arrow_left_outlined
                            : Icons.arrow_forward_ios,
                        color: Colors.white,
                        size: 16,
                      ),
                      onPressed: () {
                        setState(() {
                          _isCollapsed = !_isCollapsed;
                        });
                      },
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
