import 'package:agent_assignment_app/services/providers/auth.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class BottomUserInfo extends StatelessWidget {
  final bool isCollapsed;
  final String name;
  final String code;
  final String role;

  const BottomUserInfo({
    Key? key,
    required this.isCollapsed,
    required this.role,
    required this.name,
    required this.code,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return AnimatedContainer(
      duration: const Duration(milliseconds: 100),
      height: isCollapsed ? 70 : 50,
      width: double.infinity,
      decoration: isCollapsed
          ? BoxDecoration(
              color: Colors.white10,
              borderRadius: BorderRadius.circular(20),
            )
          : BoxDecoration(
              borderRadius: BorderRadius.circular(10),
            ),
      child: isCollapsed
          ? Center(
              child: Row(
                children: [
                  Expanded(
                    flex: 5,
                    child: Padding(
                      padding: const EdgeInsets.only(left: 13.0),
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Expanded(
                            child: Align(
                              alignment: Alignment.bottomLeft,
                              child: Text(
                                '$name $code',
                                style: const TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 18,
                                ),
                                maxLines: 1,
                                overflow: TextOverflow.clip,
                              ),
                            ),
                          ),
                          Expanded(
                            child: Text(
                              role,
                              style: const TextStyle(
                                color: Colors.grey,
                              ),
                              maxLines: 1,
                              overflow: TextOverflow.ellipsis,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                  const Spacer(),
                  Expanded(
                    flex: 2,
                    child: Padding(
                      padding: const EdgeInsets.only(right: 10),
                      child: IconButton(
                        onPressed: () {
                          Provider.of<Auth>(context, listen: false).logout();
                        },
                        icon: const Icon(
                          Icons.logout,
                          color: Colors.white,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            )
          : Column(
              children: [
                Expanded(
                  child: IconButton(
                    onPressed: () {
                      Provider.of<Auth>(context, listen: false).logout();
                    },
                    icon: const Icon(
                      Icons.logout,
                      color: Colors.white,
                      size: 18,
                    ),
                  ),
                ),
              ],
            ),
    );
  }
}
