import 'package:flutter/material.dart';
import 'package:flutter_laerlingeslogbog/global.dart';
import 'package:flutter_laerlingeslogbog/login.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Læringslogbog',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const SelectLogin(title: 'Select login'),
    );
  }
}

class SelectLogin extends StatefulWidget {
  const SelectLogin({super.key, required this.title});

  final String title;

  @override
  State<SelectLogin> createState() => _SelectLoginState();
}

class _SelectLoginState extends State<SelectLogin> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        title: Text(widget.title),
      ),
      body: Center(
        child: Center(
            child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text("mercantec",
                style: TextStyle(
                    fontWeight: FontWeight.bold, fontSize: 40, height: 2)),
            SizedBox(
              width: 300,
              child: TextButton(
                style: TextButton.styleFrom(
                  backgroundColor: Global.primaryColor,
                  foregroundColor: Colors.white,
                ),
                onPressed: () {},
                child: Text('unilogin'),
              ),
            ),
            SizedBox(
              width: 300,
              child: TextButton(
                style: TextButton.styleFrom(
                  backgroundColor: Global.primaryColor,
                  foregroundColor: Colors.white,
                ),
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const LoginPage(title: 'Login'),
                    ),
                  );
                },
                child: Text('lærer/mester'),
              ),
            ),
          ],
        )),
      ),
    );
  }
}
