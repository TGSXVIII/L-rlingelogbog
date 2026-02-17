import 'package:flutter/material.dart';
import 'package:flutter_laerlingeslogbog/global.dart';
import 'package:flutter_laerlingeslogbog/login.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Læringslogbog',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const ChooseEducation(title: 'Vælg uddannelse'),
    );
  }
}

class ChooseEducation extends StatefulWidget {
  const ChooseEducation({super.key, required this.title});

  final String title;

  @override
  State<ChooseEducation> createState() => _ChooseEducationState();
}

class _ChooseEducationState extends State<ChooseEducation> {
  String selectedEducation = 'AUTO';

  final List<String> educations = [
    'AUTO',
    'Data- og kommunikationsuddannelsen',
    'Elektriker',
    'Mekaniker',
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            const Text(
              "Mercantec",
              style: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 40,
                height: 2,
              ),
            ),

            const SizedBox(height: 20),

            SizedBox(
              width: 335,
              child: DropdownButtonFormField<String>(
                value: selectedEducation,
                decoration: InputDecoration(
                  filled: true,
                  fillColor: Colors.white,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(8),
                  ),
                ),
                items: educations.map((education) {
                  return DropdownMenuItem<String>(
                    value: education,
                    child: Text(education),
                  );
                }).toList(),
                onChanged: (value) {
                  setState(() {
                    selectedEducation = value!;
                  });
                },
              ),
            ),

            const SizedBox(height: 20),

            SizedBox(
              width: 300,
              child: TextButton(
                style: TextButton.styleFrom(
                  backgroundColor: Global.primaryColor,
                  foregroundColor: Colors.white,
                ),
                onPressed: () {
                  debugPrint('Valgt uddannelse: $selectedEducation');

                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          const LoginPage(title: 'Login'),
                    ),
                  );
                },
                child: const Text('Videre'),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
