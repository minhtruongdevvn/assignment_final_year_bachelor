// ignore_for_file: non_constant_identifier_names, constant_identifier_names

enum Environment { dev, prod }

class EnvironmentVariable {
  String idsUrl;
  String aamUrl;
  String title;
  EnvironmentVariable(
      {required this.idsUrl, required this.title, required this.aamUrl});
}

class EnvironmentConfig {
  static const String _CURRENT_ENVIRONMENT =
      String.fromEnvironment('CURRENT_ENVIRONMENT', defaultValue: '0');
  static const String IDS_CLIENT_ID =
      String.fromEnvironment('IDS_CLIENT_ID', defaultValue: 'aam.agent.client');
  static const String IDS_CLIENT_SECRET = String.fromEnvironment(
      'IDS_CLIENT_SECRET',
      defaultValue:
          'AWbQAwBcSRy4MzgpstKAEJ6u23pRabQaDBksQ_czP2XjpETPpBxWhunIh4VfP8NQ');

  static EnvironmentVariable? _envVars;
  static EnvironmentVariable get envVars {
    if (_envVars == null) {
      if (Environment.prod.index.toString() == _CURRENT_ENVIRONMENT) {
        _envVars = EnvironmentVariable(
            idsUrl: 'prod ids link', aamUrl: 'prod aam link', title: 'Prod');
      } else {
        _envVars = EnvironmentVariable(
            idsUrl: 'https://10.0.2.2:5000',
            aamUrl: "http://10.0.2.2:5117",
            title: 'Dev');
      }
    }

    return _envVars!;
  }
}
