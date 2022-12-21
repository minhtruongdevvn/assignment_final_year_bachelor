import 'package:agent_app/widgets/home_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';

class NotificationService {
  static final NotificationService _notificationService =
      NotificationService._internal();
  static final FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin =
      FlutterLocalNotificationsPlugin();
  static BuildContext? _context;
  static const AndroidNotificationDetails _androidNotificationDetails =
      AndroidNotificationDetails('agent_app', 'Agent Vie',
          channelDescription: 'Application of agent',
          importance: Importance.max,
          priority: Priority.high,
          ticker: 'ticker');
  static const NotificationDetails notificationDetails = NotificationDetails(
    android: _androidNotificationDetails,
  );

  factory NotificationService() {
    return _notificationService;
  }

  NotificationService._internal();

  Future<void> init(BuildContext context) async {
    _context = context;
    const AndroidInitializationSettings initializationSettingsAndroid =
        AndroidInitializationSettings('app_icon');

    const InitializationSettings initializationSettings =
        InitializationSettings(android: initializationSettingsAndroid);

    await flutterLocalNotificationsPlugin.initialize(initializationSettings,
        onDidReceiveNotificationResponse: onDidReceiveNotificationResponse);
  }

  Future<void> showMessage(String body, int actionId, [String? payload]) async {
    await flutterLocalNotificationsPlugin.show(
        actionId, "A Notification From Agent Vie", body, notificationDetails,
        payload: payload);
  }

  Future onDidReceiveNotificationResponse(NotificationResponse response) async {
    if ((response.id == 1 || response.id == 2) && response.payload != null) {
      Navigator.pushReplacement(
          _context!,
          MaterialPageRoute(
              builder: (BuildContext context) => HomeScreen(
                    questCode: response.payload,
                  )));
    }
  }
}
