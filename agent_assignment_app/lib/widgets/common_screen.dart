import 'package:agent_assignment_app/widgets/drawer/custom_drawer.dart';
import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:loader_overlay/loader_overlay.dart';

class CommonScreen extends StatelessWidget {
  final Widget child;

  const CommonScreen({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: const BoxDecoration(
          image: DecorationImage(
              image: AssetImage("assets/img/sdf-min.jpg"), fit: BoxFit.cover)),
      child: Scaffold(
          body: LoaderOverlay(
              useDefaultLoading: false,
              overlayWidget: const Center(
                child: SpinKitCubeGrid(
                  color: Colors.lightBlue,
                  size: 50.0,
                ),
              ),
              child: child),
          backgroundColor: Colors.transparent,
          drawer: const CustomDrawer(),
          appBar: AppBar(
            elevation: 0,
            title: const Text('Agent Assignment'),
            centerTitle: true,
          )),
    );
  }
}
