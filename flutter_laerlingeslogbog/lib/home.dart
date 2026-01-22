import 'package:flutter/material.dart';
import 'package:flutter_laerlingeslogbog/global.dart';
import 'package:flutter_laerlingeslogbog/templates/footer.dart';
import 'package:flutter_laerlingeslogbog/templates/header.dart';
import 'package:percent_indicator/percent_indicator.dart';

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
        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color.fromARGB(255, 255, 255, 255),
        ),
        useMaterial3: true,
      ),
      home: const HomePage(title: 'Home'),
    );
  }
}

class HomePage extends StatefulWidget {
  const HomePage({super.key, required this.title});
  final String title;

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  Widget build(BuildContext context) {
    final ongoingItems = List.generate(4, (i) => i);
    final measureItems = List.generate(4, (i) => i);

    return Scaffold(
      appBar: const Header(),
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            children: [
              // Top card
              Container(
                height: 160,
                padding: const EdgeInsets.all(20),
                decoration: BoxDecoration(
                  color: Global.primaryColor,
                  borderRadius: BorderRadius.circular(32),
                ),
                child: Row(
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          const Text(
                            "Dagens opgaver\nnæsten klaret!",
                            style: TextStyle(
                              color: Colors.white,
                              fontSize: 20,
                              fontWeight: FontWeight.w600,
                              height: 1.2,
                            ),
                          ),
                          const SizedBox(height: 18),
                          SizedBox(
                            height: 44,
                            child: FilledButton(
                              style: FilledButton.styleFrom(
                                backgroundColor: Colors.white,
                                foregroundColor: Global.primaryColor,
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(16),
                                ),
                                padding:
                                    const EdgeInsets.symmetric(horizontal: 22),
                              ),
                              onPressed: () {},
                              child: const Text(
                                "Se opgaver",
                                style: TextStyle(fontWeight: FontWeight.w700),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                    const SizedBox(width: 16),
                    CircularPercentIndicator(
                      radius: 46.0,
                      lineWidth: 10.0,
                      percent: 0.85,
                      animation: true,
                      circularStrokeCap: CircularStrokeCap.round,
                      backgroundColor: Colors.white.withOpacity(0.25),
                      progressColor: Colors.white,
                      center: const Text(
                        '85%',
                        style: TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.w700,
                          fontSize: 18,
                        ),
                      ),
                    ),
                  ],
                ),
              ),

              const SizedBox(height: 16),

              Align(
                alignment: Alignment.centerLeft,
                child: Text(
                  "Igangværende ${ongoingItems.length}",
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 20,
                  ),
                ),
              ),

              const SizedBox(height: 16),

              SizedBox(
                height: 100,
                child: SingleChildScrollView(
                  scrollDirection: Axis.horizontal,
                  child: Row(
                    children: List.generate(ongoingItems.length, (index) {
                      return Padding(
                        padding: EdgeInsets.only(
                          right: index == ongoingItems.length - 1 ? 0 : 12,
                        ),
                        child: Container(
                          height: 100,
                          width: MediaQuery.of(context).size.width - 200,
                          padding: const EdgeInsets.all(20),
                          decoration: BoxDecoration(
                            color: Colors.grey,
                            borderRadius: BorderRadius.circular(32),
                          ),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
                              Text(
                                "Kategori ${index + 1}",
                                style: const TextStyle(
                                  color: Colors.black,
                                  fontSize: 10,
                                  fontWeight: FontWeight.w600,
                                  height: 1.2,
                                ),
                              ),
                              const SizedBox(height: 18),
                              Text(
                                "Opgave navn ${index + 1}",
                                style: const TextStyle(
                                  color: Colors.black,
                                  fontSize: 20,
                                  fontWeight: FontWeight.bold,
                                  height: 1.2,
                                ),
                              ),
                            ],
                          ),
                        ),
                      );
                    }),
                  ),
                ),
              ),

              const SizedBox(height: 16),

              Align(
                alignment: Alignment.centerLeft,
                child: Text(
                  "Målepinde ${measureItems.length}",
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 20,
                  ),
                ),
              ),

              const SizedBox(height: 16),

              ...List.generate(measureItems.length, (index) {
                return Padding(
                  padding: const EdgeInsets.only(bottom: 16),
                  child: _MeasureCard(
                    title: "Målepindenavn ${index + 1}",
                    subtitle: "23 opgaver",
                    percent: 0.85,
                  ),
                );
              }),
            ],
          ),
        ),
      ),
      bottomNavigationBar: const Footer(),
    );
  }
}

class _MeasureCard extends StatelessWidget {
  final String title;
  final String subtitle;
  final double percent;

  const _MeasureCard({
    required this.title,
    required this.subtitle,
    required this.percent,
  });

  @override
  Widget build(BuildContext context) {
    final pctText = "${(percent * 100).round()}%";

    return Container(
      height: 80,
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
      decoration: BoxDecoration(
        color: Global.primaryColor,
        borderRadius: BorderRadius.circular(32),
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Expanded(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(
                  title,
                  maxLines: 1,
                  overflow: TextOverflow.ellipsis,
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                    height: 1.2,
                  ),
                ),
                const SizedBox(height: 4),
                Text(
                  subtitle,
                  maxLines: 1,
                  overflow: TextOverflow.ellipsis,
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 13,
                    height: 1.2,
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(width: 12),
          SizedBox(
            width: 40,
            height: 40,
            child: CircularPercentIndicator(
              radius: 20,
              lineWidth: 5,
              percent: percent.clamp(0.0, 1.0),
              animation: true,
              circularStrokeCap: CircularStrokeCap.round,
              backgroundColor: Colors.white24,
              progressColor: Colors.white,
              center: Text(
                pctText,
                style: const TextStyle(
                  color: Colors.white,
                  fontWeight: FontWeight.w700,
                  fontSize: 12,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
