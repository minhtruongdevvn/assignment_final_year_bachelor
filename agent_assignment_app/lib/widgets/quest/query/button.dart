import 'package:flutter/material.dart';

class QueryButton extends StatelessWidget {
  final String name;
  final VoidCallback onTap;
  final List<Color> colors;
  const QueryButton(
      {super.key,
      required this.name,
      required this.onTap,
      this.colors = const [
        Color.fromRGBO(185, 43, 39, 1),
        Color.fromRGBO(21, 101, 192, 1),
      ]});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        height: 53,
        width: double.infinity,
        margin: const EdgeInsets.symmetric(horizontal: 10),
        alignment: Alignment.center,
        decoration: BoxDecoration(
            boxShadow: [
              BoxShadow(
                  blurRadius: 4,
                  color: Colors.black12.withOpacity(.2),
                  offset: const Offset(2, 2))
            ],
            borderRadius: BorderRadius.circular(100)
                .copyWith(bottomRight: const Radius.circular(0)),
            gradient: LinearGradient(colors: colors)),
        child: Text(name,
            style: TextStyle(
                color: Colors.white.withOpacity(.8),
                fontSize: 15,
                fontWeight: FontWeight.bold)),
      ),
    );
  }
}
