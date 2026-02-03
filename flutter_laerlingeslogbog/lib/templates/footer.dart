import 'package:flutter/material.dart';
import 'package:flutter_laerlingeslogbog/global.dart';
import 'package:flutter_laerlingeslogbog/home.dart';
import 'package:flutter_laerlingeslogbog/startTask.dart';

class Footer extends StatelessWidget {
  const Footer({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Global.secondaryColor,
      height: 60,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              IconButton(
                padding: EdgeInsets.zero,
                constraints: const BoxConstraints(),
                onPressed: () => Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => const HomePage(
                            title: 'home',
                          )),
                ),
                icon: Icon(Icons.cottage, color: Global.primaryColor),
              ),
            ],
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              IconButton(
                padding: EdgeInsets.zero,
                constraints: const BoxConstraints(),
                onPressed: () => Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => const HomePage(
                            title: 'home',
                          )),
                ),
                icon: Icon(Icons.calendar_month, color: Global.primaryColor),
              ),
            ],
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              IconButton(
                padding: EdgeInsets.zero,
                constraints: const BoxConstraints(),
                iconSize: 50,
                onPressed: () => Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => const StartTask(
                            title: 'startTask',
                          )),
                ),
                icon:
                    Icon(Icons.add_circle_rounded, color: Global.primaryColor),
              ),
            ],
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              IconButton(
                padding: EdgeInsets.zero,
                constraints: const BoxConstraints(),
                onPressed: () => Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => const HomePage(
                            title: 'home',
                          )),
                ),
                icon: Icon(Icons.article_outlined, color: Global.primaryColor),
              ),
            ],
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              IconButton(
                padding: EdgeInsets.zero,
                constraints: const BoxConstraints(),
                onPressed: () => Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => const HomePage(
                            title: 'home',
                          )),
                ),
                icon: Icon(Icons.person, color: Global.primaryColor),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
