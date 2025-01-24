# Ping Pong

## Uvod

PingPong je aplikacija koja omogućava igranje igre ping pong u digitalnom okruženju. PingPong je dizajniran za korisnike svih uzrasta koji žele da se opuste i zabave uz jednostavnu i uzbudljivu igru. Osmišljena je kao desktop aplikacija razvijena u C# programskom jeziku koristeći WPF (Windows Presentation Foundation) razvojni okvir. Za upotrebu aplikacije potreban je desktop ili laptop računar sa Windows operativnim sistemom i minimalnim hardverskim specifikacijama.

### Modovi igre

* Single Player - U ovom modu, igrač ima priliku da igra protiv računarskog protivnika. Ovakav način igre predstavlja izazov, ali i omogućava usavršavanje vejština igrača. Single Player mod je idealan za vejžbanje i uživanje u igri u solo režimu.
* Two Player - Ovaj mod omogućava takmičenje između dva igrača na istom uređaju. Svaki igrač kontroliše svoj reket pomoću različitih tastera na tastaturi, što pruža dinamično i zabavno iskustvo za prijatelje, kolege ili članove porodice koji žele da se oprobaju u ping pong meču.

## Korisničko uputstvo

### Pokretanje aplikacije

Prilikom otvaranja aplikacije, korisniku se prikazuje početni ekran aplikacije na kome korisnik ima mogućnost započinjanja igre u jednom od dva moguća moda. Pored toga ima mogućnost ulaska u meni klikom na simbol menija u gornjem lijevom uglu ekrana te ima mogućnost izlaska iz menija klikom na dugme _Exit Game_.

![image](https://github.com/user-attachments/assets/07f69389-bf64-40a8-9e81-177f0e789cea)

### Podešavanje aplikacije

Klikom na dugme za meni korisniku se prikazuje sledeći ekran:

![image](https://github.com/user-attachments/assets/a1481ca4-603f-46a2-b62f-a11481ffb10b)

Korisnik u meniju ima mogućnost da promjeni temu aplikacije (svijetla i tamna tema) klikom na dugme _Light/Dark Theme_. 

![image](https://github.com/user-attachments/assets/ba6b3009-e10a-4fc6-a62d-8d10b7ae18ed)

Takođe korisnik ima mogućnost isključivanja i uključivanja zvuka udara loptice o reket klikom na dugme _Toggle Game Sound_ kao i mogućnost isključivanja i uključivanja pozadinske muzike klikom na dugme _Toggle Music_.

### Pregled ostvarenih rezultata

Klikom na dugme _View Scores_ u meniju korisniku se prikazuje ekran na su prikazani svi njegovi prethodno postignuti rezultati.

![image](https://github.com/user-attachments/assets/0df250af-646a-470d-a66b-3ee95dd732d3)

Korisnik može da pregleda odvojene rezultate za svaki od modova igranja. Takodje korisnik može da obriše sve rezultate klikom na dugme _Clear All Scores_.

### Single Player mod igre

Klikom na dugme _Single Player_ na početnom ekranu korisniku se prikazuje sledeći ekran:

![image](https://github.com/user-attachments/assets/1893ff9c-132f-41d9-a83f-52367ae6c9a6)

Na lijevoj strani ekrana se nalazi reket kojim upravlja igrač dok se na desnoj strani ekrana nalazi reket kojim upravlja računar. Na ovom ekranu se prikazuje trenutni rezultat Igrača u lijevom donjem uglu ekrana i računara u donjem desnom uglu ekrana. Na ekranu se prikazuje proteklo vrijeme od početka meča. U gornjem desnom uglu ekrana se nalazi dugme _Pause_ čijim se klikom vrši pauziranje igre. U gornjem lijevom uglu se nalazi dugme _Back_ kojim se završava trenutni meč i korisnik se vraća na početni ekran.

Za upravljanje reketom Igrač može da koristi _W_ i _S_ kao i _PgUp_ i _PgDn_ (strelice) tastere na tastaturi.

Na sledećoj slici je prikazan izgled ekrana u slučaju pauzirane igre:

![image](https://github.com/user-attachments/assets/33ce1c05-d6fa-44c3-943a-64ba438f77c9)

Klikom na dugme _Resume_ korisnik može da nastavi igru.

### Two Player mod igre

Klikom na dugme _Two Player_ na početnom ekranu korisniku se prikazuje sledeći ekran:

![image](https://github.com/user-attachments/assets/849eedd6-990e-4a90-89f4-b7b3f466d08b)

Izgled ovog ekrana je gotovo identičan izgledu ekrana za Single Player mod s tim što su na ovom ekranu prikazani rezultat za Igrača 1 u lijevom donjem uglu i a Igrača 2 u donjem desnom uglu ekrana. Sve ostale funkcionalosti i zgled su opisnai u dijelu za Single Plazer mod ige.

Igrač 1 upravlja lijevim reketom i za to koristi _W_ i _S_ tastere na tastaturi a Igrač 2 upravlja desnim reketom i za to koristi _PgUp_ i _PgDn_ (strelice) tastere na tastaturi. 
