import 'package:agent_assignment_app/services/providers/auth.dart';
import 'package:agent_assignment_app/services/providers/category.dart';
import 'package:agent_assignment_app/widgets/agent/screen.dart';
import 'package:agent_assignment_app/widgets/auth/authenticate_screen.dart';
import 'package:agent_assignment_app/widgets/operator_create.dart';
import 'package:agent_assignment_app/widgets/quest/detail/screen.dart';
import 'package:agent_assignment_app/widgets/quest/screen.dart';
import 'package:agent_assignment_app/widgets/splash_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_easyloading/flutter_easyloading.dart';
import 'package:provider/provider.dart';
import 'package:window_manager/window_manager.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  // Must add this line.
  await windowManager.ensureInitialized();
  await WindowManager.instance.setFullScreen(true);

  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    // return MaterialApp(
    //   title: 'Agent Assignment',
    //   theme: ThemeData(
    //     scrollbarTheme: Theme.of(context).scrollbarTheme.copyWith(
    //         thumbColor: MaterialStateProperty.all<Color>(
    //             Colors.white.withOpacity(0.5))),
    //     textSelectionTheme: const TextSelectionThemeData(
    //       cursorColor: Colors.white,
    //       selectionColor: Colors.green,
    //       selectionHandleColor: Colors.blue,
    //     ),
    //     primarySwatch: Colors.blue,
    //     colorScheme: Theme.of(context).colorScheme.copyWith(
    //         secondary: const Color.fromRGBO(4, 45, 107, 1),
    //         primary: const Color.fromRGBO(4, 13, 33, 1)),
    //     fontFamily: 'Raleway',
    //     textTheme: Theme.of(context).textTheme.copyWith().apply(
    //         bodyColor: Colors.white,
    //         displayColor: Colors.white,
    //         decorationColor: Colors.white),
    //   ),
    //   home: QuestDetailScreen(),
    // );
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (context) => Auth()),
        ChangeNotifierProvider(
          create: (context) => CateogryProvider(),
        )
      ],
      child: Consumer<Auth>(
          builder: (context, auth, child) => MaterialApp(
                  builder: EasyLoading.init(),
                  title: 'Agent Assignment',
                  theme: ThemeData(
                    scrollbarTheme: Theme.of(context).scrollbarTheme.copyWith(
                        thumbColor: MaterialStateProperty.all<Color>(
                            Colors.white.withOpacity(0.5))),
                    textSelectionTheme: const TextSelectionThemeData(
                      cursorColor: Colors.white,
                      selectionColor: Colors.green,
                      selectionHandleColor: Colors.blue,
                    ),
                    primarySwatch: Colors.blue,
                    colorScheme: Theme.of(context).colorScheme.copyWith(
                        secondary: const Color.fromRGBO(4, 45, 107, 1),
                        primary: const Color.fromRGBO(4, 13, 33, 1)),
                    fontFamily: 'Raleway',
                    textTheme: Theme.of(context).textTheme.copyWith().apply(
                        bodyColor: Colors.white, displayColor: Colors.white),
                  ),
                  home: auth.isAuth
                      ? const QuestScreen()
                      : FutureBuilder(
                          future: auth.tryAutoLogin(),
                          builder: (ctx, authResultSnapshot) =>
                              authResultSnapshot.connectionState ==
                                      ConnectionState.waiting
                                  ? const SplashScreen()
                                  : const AuthenticateScreen(),
                        ),
                  routes: {
                    QuestScreen.route: (ctx) => const QuestScreen(),
                    AgentScreen.route: (ctx) => const AgentScreen(),
                    OperatorCreate.route: (ctx) => const OperatorCreate(),
                    QuestDetailScreen.route: (ctx) => QuestDetailScreen()
                  })),
    );
  }
}
