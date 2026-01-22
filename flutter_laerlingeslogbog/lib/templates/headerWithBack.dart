import 'package:flutter/material.dart';

class HeaderWithBack extends StatelessWidget implements PreferredSizeWidget {
  const HeaderWithBack({super.key});

  @override
  Size get preferredSize => const Size.fromHeight(60);

  @override
  Widget build(BuildContext context) {
    return AppBar(
      backgroundColor: Colors.white,
      elevation: 0,
      automaticallyImplyLeading: false,
      title: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          IconButton(
            onPressed: () => Navigator.pop(context),
            icon: const Icon(Icons.keyboard_backspace_rounded),
          ),
          const Text(
            "Dagens opgaver",
            style: TextStyle(color: Colors.black, fontWeight: FontWeight.bold),
          ),
          IconButton(
            onPressed: () {},
            icon: const Icon(Icons.notifications),
          ),
        ],
      ),
    );
  }
}
