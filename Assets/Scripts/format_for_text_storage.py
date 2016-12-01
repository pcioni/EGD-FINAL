def format(s):
    v= s.split('\n');
    for line in v:
        if line == "":
            continue;
        line = line.strip();
        if not ':' in line:
            line = "|"+line;
        line = line.replace(':','|');
        line = '"'+line+'"';
        line = line.replace('\xe2\x80\x99',"'");
        line = line.replace('\xe2\x80\xa6', '...');
        print (line+",");