# Word data

Shipped with Lyricist and copied next to the exe. All free to redistribute; licences beside them.

| File | What | Source |
| --- | --- | --- |
| `cmudict.dict.gz` | CMU Pronouncing Dictionary: ~135,000 words, American pronunciation (ARPAbet, stress 0/1/2) | github.com/cmusphinx/cmudict (commit 7479086), BSD-style licence |
| `britfone.main.csv.gz` | Britfone 3.0.1: ~16,000 words incl. the 10,000 most frequent, British RP (IPA) | github.com/JoseLlarena/Britfone, MIT |
| `th_en_US_v2.dat.gz` | LibreOffice English thesaurus (MyThes format, built from WordNet 3.0) | github.com/LibreOffice/dictionaries `en/` (commit c0865a8), WordNet licence |

UK pronunciations = Britfone where it has the word, else the CMU entry converted to British
(non-rhotic: R after a vowel dropped; US "oh" -> "əʊ", LOT -> "ɒ", etc.). US = CMU.
Britfone's words also mark which words are "common" (rare ones are hidden unless asked for).
