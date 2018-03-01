# CattleEnd
ASP.Net MVC web application for managing fictional warriors and schedule associated guard days. A show-off project.

### The story
Once upon a time there lived a fellowship of great warriors. A fine folks they were – brave,
cunning and steadfast...Men, elves, dwarves, even some hobbits in the fellowship there were.
Each day they fought evil and defended the realm of CattleEnd from all kind of nasty beings and
dark forces. And each day one of them needed to guard the realm from night raids of those
malevolent things. There was several posts along the kingdom borders that needed to be
scouted each night in order to secure the realm. It was a tough job – those night watches - and
though they were all brave, no one would do it with ease. Therefore they decided to
automatically arrange themselves for those night expeditions. They decided to build a machine
that will for each day choose one of them for the night watch. They employed the great Istari
Wizards of the time to build them such machine, but the wizards, tricky as they always were,
decided to build them a universal machine that could do anything but needs to be configured
first. Since the wizards left little instructions to configure such a machine they needed someone
to configure it for them.
The brave fellowship now needs you to help them protect their borders! Do you have what it
takes to help the CattleEnd and maybe even become one of the brave defenders of the realm?!
The wizards left little instructions on how to use machine, but they managed to write several
words in Ithildin. Those words said: “ASP.MVC”. The folks of the CattleEnd had but a clue what
that meant so they asked you to decipher them to the Common language and use them in
configuring their much needed machine! If you agree to configure the machine, the fellowship will leave you with following
requirements.

### The requirements
1. We want to dynamically add/remove brave warriors to the machine. New warriors and current
ones should be manually added to the machine and their guard days should be calculated
accordingly
1. When added to list, the guards are sorted by their name alphabetically and the schedule is
created by iterating through the list. When the last guard has done his guard post, the list is
again traversed from the beginning
1. For each guard the machine should accept their “SMPT signatures” so the machine could remind
them each day that today is their day to guard the realm. Those notifications are delivered to
the each warrior via SMTP sub-dimensions on 15:00 hours
1. If a guard falls asleep during or before his guard time, the fellowship will sanction that by
assigning him additional guard day. The machine should be able to accept such input and after
doing so it should find the first next day that person should be a guard and insert additional one
right after that day (the guard person for that additional plan is not removed, but rather the
whole schedule is shifted for one day forward)
1. After adding or removing new warriors or adding additional days for each of them, the machine
should automatically calculate and store the new schedule. That calculation is done “on the fly”
meaning that as much of the current schedule as possible should be preserved
1. The machine should also display the list of scheduled duties, so the warriors can adequately
prepare several days ahead for their duties

Please be so kind and help the people of CattleEnd and their brave warriors in this quest...if the machine
is configured properly, they will strongly consider on generously awarding the creator! Also feel free to
expand the features of the machine with anything you find useful.
