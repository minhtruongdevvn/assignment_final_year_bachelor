String displayFromSnakeStyle(String originText) {
  if (originText.isEmpty) {
    return '';
  }
  return "${originText[0].toUpperCase()}${originText.substring(1)}"
      .replaceAll(RegExp(r'(_|-)+'), ' ');
}
